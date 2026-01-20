using CA_Application;
using CA_Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Endpoints.Drink;

[ApiController]
[Route("api/ingredient")]

public class IngredientEndpoints(IIngredientService ingredientService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetIngredientById(int id)
    {
        var ingredient = await ingredientService.GetIngredientByIdAsync(id);
        if (ingredient is null)
            return NotFound();

        return Ok(ingredient);
    }

    [HttpPost]
    public async Task<IActionResult> AddIngredient([FromBody] AddIngredientDTO dto)
    {
        var ingredient = await ingredientService.AddIngredientAsync(dto);

        return CreatedAtAction(nameof(GetIngredientById), new { id = ingredient.Id }, ingredient);
    }



    [HttpGet]
    public async Task<IActionResult> GetIngredients([FromQuery] GetIngredientsDTO dto)
    {
        var result = await ingredientService.GetIngredientsAsync(dto);
        return Ok(result);
    }
}