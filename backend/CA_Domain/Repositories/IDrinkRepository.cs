using CA_Domain.Entities;

namespace CA_Domain.Repositories
{
    public interface IDrinkRepository
    {
        Task<Drink> AddDrinkAsync(Drink drink);
    }
}