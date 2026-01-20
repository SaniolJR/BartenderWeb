using System.ComponentModel.DataAnnotations;
using CA_Domain.Entities;

namespace CA_Application.DTOs
{
    public class AddIngredientDTO
    {
        [Required(ErrorMessage = "Insert a Ingredient Name!")]
        public string Name { get; set; } = default!;
        public List<Drink> Drinks { get; set; } = new List<Drink>();
    }
}