using System.ComponentModel.DataAnnotations;

namespace UI.Models;

public class RegisterFormVM
{
    [Required] public string Name { get; set; }

    [Required(ErrorMessage = "Empty email")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Empty password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string ConfirmPassword { get; set; }
    public bool Agreement { get; set; }
}