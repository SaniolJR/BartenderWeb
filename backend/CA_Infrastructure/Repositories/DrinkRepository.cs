using CA_Domain.Entities;
using CA_Domain.Repositories;
using CA_Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CA_Infrastructure.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly MainDbContext _dbContext;

        public DrinkRepository(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Drink> GetDrinkByIdAsync(int id)
        {
            return await _dbContext.Drinks.FindAsync(id);
        }
        public async Task<Drink> AddDrinkAsync(Drink drink)
        {
            _dbContext.Drinks.Add(drink);
            await _dbContext.SaveChangesAsync();
            return drink;
        }

        public async Task<List<Drink>> GetDrinksAsync(bool verified, string textFilter, int missingIngredients, List<string> inputIngredients)
        {
            // 1. Jeśli nie podano składników - prosta sprawa
            if (inputIngredients == null || inputIngredients.Count == 0)
            {
                return await _dbContext.Drinks
                    .Include(d => d.Ingredients) // WAŻNE: Załaduj składniki, żeby frontend je widział
                    .Where(d => (!verified || d.Verified) &&
                                (string.IsNullOrEmpty(textFilter) || d.Name.ToLower().Contains(textFilter.ToLower())))
                    // Uwaga: StringComparison w LINQ do SQL często rzuca błędem, bezpieczniej użyć ToLower()
                    .ToListAsync();
            }

            // 2. Jeśli są składniki - Podejście Zoptymalizowane
            // Pobieramy od razu wszystkie drinki, które zawierają PRZYNAJMNIEJ JEDEN z wymienionych składników
            // To jest JEDNO zapytanie do bazy.

            var candidates = await _dbContext.Drinks
                .Include(d => d.Ingredients) // Ładujemy składniki drinka, żeby policzyć "brakujące"
                .Where(d => (!verified || d.Verified)) // Filtr verified
                .Where(d => string.IsNullOrEmpty(textFilter) || d.Name.ToLower().Contains(textFilter.ToLower())) // Filtr nazwy
                .Where(d => d.Ingredients.Any(i => inputIngredients.Contains(i.Name))) // Tylko drinki, które mają coś z naszej listy
                .ToListAsync();

            // 3. Logika filtrowania w pamięci (Memory)
            // Skoro mamy już kandydatów w RAM-ie, liczymy brakujące składniki szybkim C#

            var result = new List<Drink>();

            // Zróbmy HashSet dla wydajności (szybsze sprawdzanie Contains)
            var userIngredientsSet = new HashSet<string>(inputIngredients, StringComparer.OrdinalIgnoreCase);

            foreach (var drink in candidates)
            {
                // Ile składników tego drinka user POSIADA?
                int havingCount = drink.Ingredients.Count(i => userIngredientsSet.Contains(i.Name));

                // Ile brakuje? (Wszystkie wymagane - te co mamy)
                int missingCount = drink.Ingredients.Count - havingCount;

                // Sprawdzamy warunek (np. brakuje max 2)
                if (missingCount <= missingIngredients)
                {
                    result.Add(drink);
                }
            }

            return result;
        }


        public async Task<Ingredient> GetIngredientByNameAsync(string name)
        {
            return await _dbContext.Ingredients
                .FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower());
        }
    }
}