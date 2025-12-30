using System.ComponentModel.DataAnnotations;

namespace CA_Application.DTOs
{
    public class AddDrinkDTO
    {
        [Required(ErrorMessage = "Insert a Drink Name!")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Insert a Drink Receipe!")]
        public string Receipe { get; set; } = default!;
        public string? ImageUrl { get; set; }
    }
}