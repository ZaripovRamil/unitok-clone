using System.ComponentModel.DataAnnotations;
namespace UI.Models;

public class ResetPasswordVM
{
    [Required(ErrorMessage = "Empty password")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string? ConfirmPassword { get; set; }
    
    public string UserId { get; set; }
    public string Code { get; set; }
}