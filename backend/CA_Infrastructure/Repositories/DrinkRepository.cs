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
            //if no ingredients
            if (inputIngredients == null || inputIngredients.Count == 0)
            {
                return await _dbContext.Drinks
                    .Include(d => d.Ingredients) //get ingredients
                    .Where(d => (!verified || d.Verified) &&
                                (string.IsNullOrEmpty(textFilter) || d.Name.ToLower().Contains(textFilter.ToLower())))
                    .ToListAsync();
            }

            // if ingredients
            // get all drinks from DB that have at least one ingredient by one query

            var candidates = await _dbContext.Drinks
                .Include(d => d.Ingredients) // get drink ingredients to count how many missing
                .Where(d => (!verified || d.Verified)) // verified filter
                .Where(d => string.IsNullOrEmpty(textFilter) || d.Name.ToLower().Contains(textFilter.ToLower())) // name filrer
                .Where(d => d.Ingredients.Any(i => inputIngredients.Contains(i.Name))) // only drinks that have our ingredient
                .ToListAsync();

            // filter drinks and reduce to missing count friendly

            var result = new List<Drink>();

            var userIngredientsSet = new HashSet<string>(inputIngredients, StringComparer.OrdinalIgnoreCase);

            foreach (var drink in candidates)
            {
                // how many ingredients of this drink user have
                int havingCount = drink.Ingredients.Count(i => userIngredientsSet.Contains(i.Name));

                // missing count validation
                int missingCount = drink.Ingredients.Count - havingCount;

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