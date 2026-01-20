using CA_Domain.Entities;
using CA_Domain.Repositories;
using AutoMapper;
using CA_Application.DTOs;

namespace CA_Application
{
    public interface IDrinkService
    {
        Task<Drink> GetDrinkByIdAsync(int id);
        Task<Drink> AddDrinkAsync(AddDrinkDTO dto);
        Task<List<Drink>> GetDrinksAsync(GetDrinksDTO dto);
        Task<Ingredient> GetIngredientByNameAsync(string name);

    }
}