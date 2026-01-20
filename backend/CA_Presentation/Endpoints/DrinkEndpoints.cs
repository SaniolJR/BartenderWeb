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

        return Ok(drink);
    }

    [HttpPost]
    public async Task<IActionResult> AddDrink([FromBody] AddDrinkDTO dto)
    {
        var drink = await drinkService.AddDrinkAsync(dto);
        if (dto.Ingredients == null || dto.Ingredients.Count < 2)
        {
            return BadRequest("Drink must have at least 2 ingredients.");
        }
        return CreatedAtAction(nameof(GetDrinkById), new { id = drink.Id }, drink);
    }

    [HttpGet]
    public async Task<IActionResult> GetDrinks([FromQuery] GetDrinksDTO dto)
    {
        var result = await drinkService.GetDrinksAsync(dto);
        return Ok(result);
    }
}
