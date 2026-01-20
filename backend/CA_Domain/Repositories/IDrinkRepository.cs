using CA_Domain.Entities;

namespace CA_Domain.Repositories
{
    public interface IDrinkRepository
    {
        Task<Drink> GetDrinkByIdAsync(int id);
        Task<Drink> AddDrinkAsync(Drink drink);
        Task<List<Drink>> GetDrinksAsync(bool Verified, string TextFilter,
                     int MissingIngredients, List<Ingredient> ingredients);

        Task<Ingredient> GetIngredientByNameAsync(string name);
    }
}