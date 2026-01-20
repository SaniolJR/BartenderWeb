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
        return CreatedAtAction(nameof(GetDrinkById), new { id = drink.Id }, drink);
    }

    [HttpGet]
    public async Task<IActionResult> GetDrinks([FromBody] GetDrinksDTO dto)
    {
        var result = await drinkService.GetDrinksAsync()
    }
}
