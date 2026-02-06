using CA_Application;
using CA_Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Drink;

[ApiController]
[Route("api/drinks")]
public class DrinkEndpoints(IDrinkService drinkService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDrinkById(int id)
    {
        var drink = await drinkService.GetDrinkByIdAsync(id);
        if (drink is null)
            return NotFound();
        var dto = new DrinkDTO
        {
            Id = drink.Id,
            Name = drink.Name,
            Recipe = drink.Recipe,
            Ingredients = drink.Ingredients.Select(i => new IngredientDTO { Id = i.Id, Name = i.Name }).ToList(),
            AverageRating = drink.AverageRating,
            Verified = drink.Verified,
            ImageURL = drink.ImageURL
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> AddDrink([FromBody] AddDrinkDTO dto)
    {
        try
        {
            var id = await drinkService.AddDrinkAsync(dto);

            return CreatedAtAction(nameof(GetDrinkById), new { id = id }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Server Error.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetDrinks([FromQuery] GetDrinksDTO query)
    {
        var drinks = await drinkService.GetDrinksAsync(query);
        // mapping back to DTO for avoid jamming lists
        var dtoList = drinks.Select(d => new DrinkDTO
        {
            Id = d.Id,
            Name = d.Name,
            Recipe = d.Recipe,
            Ingredients = d.Ingredients.Select(i => new IngredientDTO { Id = i.Id, Name = i.Name }).ToList(),
            AverageRating = d.AverageRating,
            Verified = d.Verified,
            ImageURL = d.ImageURL
        }).ToList();
        return Ok(dtoList);
    }
}
