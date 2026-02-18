using System.ComponentModel.DataAnnotations;
using CA_Domain.Entities;

namespace CA_Application.DTOs
{
    public class RegisterAccDTO
    {
        [Required(ErrorMessage = "Insert a Username!")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "Insert a Password!")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$",
            ErrorMessage = "Password must have at least 8 characters, one number, and one special character!"
        )]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "Insert email!")]
        [EmailAddress(ErrorMessage = "Invalid email format!")]
        public string Email { get; set; } = default!;

    }
}