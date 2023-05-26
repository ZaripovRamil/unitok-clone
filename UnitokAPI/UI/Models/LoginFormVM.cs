using System.ComponentModel.DataAnnotations;

namespace UI.Models;

public class LoginFormVM
{
    [Required(ErrorMessage = "Empty email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Empty password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me")] public bool RememberMe { get; set; }
}