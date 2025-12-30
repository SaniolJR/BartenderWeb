using CA_Domain.Entities;
using CA_Domain.Repositories;
using AutoMapper;
using CA_Application.DTOs;

namespace CA_Application
{
    internal class DrinkService(IDrinkRepository drinkRepository, IMapper mapper) : IDrinkService
    {
        public async Task<Drink> GetDrinkByIdAsync(int id)
        {
            return await drinkRepository.GetDrinkByIdAsync(id);
        }
        public async Task<Drink> AddDrinkAsync(AddDrinkDTO dto)
        {
            var drink = mapper.Map<Drink>(dto);
            return await drinkRepository.AddDrinkAsync(drink);
        }
    }
}