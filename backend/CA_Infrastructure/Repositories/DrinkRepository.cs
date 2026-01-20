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

        public async Task<List<Drink>> GetDrinksAsync(bool Verified, string TextFilter,
                     int MissingIngredients, List<string> Ingredients)
        {
            //getting drinks from DB
            //using N;N relation between drinks and ingredients:
            //   we can search from ingredients which drinks we should return

            var DrinksHavingIngredients = new Dictionary<Drink, int>();

            foreach (var ingName in Ingredients)
            {
                //get ingredient from DB
                var ingFromDb = await _dbContext.Ingredients.FirstOrDefaultAsync(i => i.Name == ingName);
                if (ingFromDb == null || ingFromDb.Drinks == null)
                    continue;

                //iterate through drinks 
                foreach (var drink in ingFromDb.Drinks)
                {
                    //skip non-verified drinks for only-verified filter
                    if (Verified && drink.Verified == false)
                        continue;
                    if (!string.IsNullOrEmpty(TextFilter) &&
                        !drink.Name.Contains(TextFilter, StringComparison.OrdinalIgnoreCase))
                        continue;
                    if (!DrinksHavingIngredients.ContainsKey(drink))
                    {
                        DrinksHavingIngredients.Add(drink, 1);
                    }
                    else
                    {
                        DrinksHavingIngredients[drink]++;
                    }
                }
            }
            var result = new List<Drink>();
            foreach (var obj in DrinksHavingIngredients)
            {
                var drink = obj.Key;
                var cnt = obj.Value;
                if (drink.Ingredients.Count - cnt <= MissingIngredients)
                    result.Add(drink);
            }
            return result;
        }

        async Task<Ingredient> GetIngredientByNameAsync(string name)
        {
            return await _dbContext.Ingredients.FirstOrDefaultAsync(i => i.Name == name);
        }
    }
}