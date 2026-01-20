using CA_Domain.Entities;
using CA_Domain.Repositories;
using CA_Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CA_Infrastructure.Repositories;

public class IngredientRepository : IIngredientRepository
{

    private readonly MainDbContext _dbContext;

    public IngredientRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Ingredient> GetIngredientByIdAsync(int id)
    {
        return await _dbContext.Ingredients.FindAsync(id);
    }
    public async Task<Ingredient> AddIngredientAsync(Ingredient ingredient)
    {
        _dbContext.Ingredients.Add(ingredient);
        await _dbContext.SaveChangesAsync();
        return ingredient;
    }

    public async
        Task<List<Ingredient>> GetIngredientsAsync(string filter)
    {
        return await _dbContext.Ingredients
        .Where(i => string.IsNullOrEmpty(filter) ||
         i.Name.ToLower().Contains(filter.ToLower())) // name filrer
        .ToListAsync();
    }
}