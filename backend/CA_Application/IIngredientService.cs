using CA_Application.DTOs;
using CA_Domain.Entities;
using CA_Domain.Repositories;
using AutoMapper;

namespace CA_Application;

public interface IIngredientService
{
    Task<Ingredient> GetIngredientByIdAsync(int id);
    Task<Ingredient> AddIngredientAsync(AddIngredientDTO dto);
    Task<List<Ingredient>> GetIngredientsAsync(GetIngredientsDTO dto);
}