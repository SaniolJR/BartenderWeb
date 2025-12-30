using CA_Domain.Entities;

namespace CA_Domain.Repositories
{
    public interface IDrinkRepository
    {
        Task<Drink> GetDrinkByIdAsync(int id);
        Task<Drink> AddDrinkAsync(Drink drink);
    }
}