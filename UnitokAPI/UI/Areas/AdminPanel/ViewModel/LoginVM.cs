using System.ComponentModel.DataAnnotations;

namespace UI.Areas.AdminPanel.ViewModel;

public class LoginVM
{
    [Required(ErrorMessage = "Empty email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Empty password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}