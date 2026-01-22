using System.ComponentModel.DataAnnotations;
using CA_Domain.Entities;

namespace CA_Application.DTOs
{
    public class AddDrinkDTO
    {
        [Required(ErrorMessage = "Insert a Drink Name!")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Insert a Drink !")]
        public string Recipe { get; set; } = default!;
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Cant drink air!")]
        [MinLength(2, ErrorMessage = "Drink must have at least 2 ingredients!")]
        public List<string> Ingredients { get; set; } = default!;
    }
}