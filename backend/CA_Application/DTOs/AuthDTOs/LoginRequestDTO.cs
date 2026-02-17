using System.ComponentModel.DataAnnotations;
using CA_Domain.Entities;

namespace CA_Application.DTOs
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "Insert a Username!")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "Insert a Password!")]
        public string Password { get; set; }
    }
}