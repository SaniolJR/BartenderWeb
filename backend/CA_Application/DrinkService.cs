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
            //get ingredients drom dto and db
            var uniqueInputNames = dto.Ingredients.Distinct().ToList();
            var existingIngredients = await drinkRepository
                .GetIngredientsByNamesAsync(uniqueInputNames);

            if (existingIngredients.Count != uniqueInputNames.Count)
            {
                // get missing ingretienrs
                var foundNames = existingIngredients.Select(i => i.Name.ToLower());
                var missing = uniqueInputNames.Where(n => !foundNames.Contains(n.ToLower()));

                throw new ArgumentException($"Coudnt find ingredients: {string.Join(", ", missing)}");
            }

            if (existingIngredients.Count < 2)
            {
                throw new ArgumentException("Drink must contains at least 2 ingredients.");
            }

            var drink = mapper.Map<Drink>(dto);
            drink.Ingredients = existingIngredients;

            return await drinkRepository.AddDrinkAsync(drink);
        }

        public async Task<List<Drink>> GetDrinksAsync(GetDrinksDTO dto)
        {

            return await drinkRepository.GetDrinksAsync(
                dto.Verified,
                dto.TextFilter,
                dto.MissingIngredients,
                dto.Ingredients ?? new List<string>(),
                Math.Max(dto.Page, 1),
                dto.PageSize
            );
        }

        public async Task<Ingredient> GetIngredientByNameAsync(string name)
        {
            return await drinkRepository.GetIngredientByNameAsync(name);
        }

        public async Task<List<Ingredient>> GetIngredientsByNamesAsync(List<string> names)
        {
            return await drinkRepository.GetIngredientsByNamesAsync(names);
        }
    }
}