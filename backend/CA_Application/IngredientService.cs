using CA_Domain.Entities;
using CA_Domain.Repositories;
using AutoMapper;
using CA_Application.DTOs;

namespace CA_Application;

internal class IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
                                : IIngredientService
{
    public async Task<Ingredient> GetIngredientByIdAsync(int id)
    {
        return await ingredientRepository.GetIngredientByIdAsync(id);
    }
    public async Task<Ingredient> AddIngredientAsync(AddIngredientDTO dto)
    {
        var ingredient = mapper.Map<Ingredient>(dto);
        return await ingredientRepository.AddIngredientAsync(ingredient);
    }

    public async Task<List<Ingredient>> GetIngredientsAsync(GetIngredientsDTO dto)
    {
        return await ingredientRepository.GetIngredientsAsync(dto.TextFilter, Math.Max(dto.Page, 1), dto.PageSize);
    }
}