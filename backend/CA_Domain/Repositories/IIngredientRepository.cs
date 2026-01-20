using CA_Domain.Entities;

namespace CA_Domain.Repositories
{
    public interface IIngredientRepository
    {
        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task<Ingredient> AddIngredientAsync(Ingredient ingredient);
    }
}