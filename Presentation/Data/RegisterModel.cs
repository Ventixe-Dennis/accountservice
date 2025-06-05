using System.ComponentModel.DataAnnotations;

namespace Presentation.Data;

public class RegisterModel
{

    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]   
    public string Password { get; set; } = null!;

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmedPassword { get; set; } = null!;


}
