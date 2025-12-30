using CA_Domain.Entities;
using CA_Domain.Repositories;
using CA_Infrastructure.Database;

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
    }
}