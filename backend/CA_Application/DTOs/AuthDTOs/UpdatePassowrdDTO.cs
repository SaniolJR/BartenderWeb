using System.ComponentModel.DataAnnotations;
using CA_Domain.Entities;

namespace CA_Application.DTOs;

public class UpdatePasswordDTO
{
    [Required(ErrorMessage = "Insert old password!")]
    public string OldPassword { get; set; } = default!;

    [Required(ErrorMessage = "Insert new password!")]
    [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$",
            ErrorMessage = "Password must have at least 8 characters, one number, and one special character!"
        )]
    public string NewPassword { get; set; } = default!;
}