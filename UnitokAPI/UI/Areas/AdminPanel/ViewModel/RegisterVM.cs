using System.ComponentModel.DataAnnotations;

namespace UI.Areas.AdminPanel.ViewModel;

public class RegisterVM
{
    [Required] public string Name { get; set; }

    [Required(ErrorMessage = "Empty email")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Empty password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public bool TermsAgreement { get; set; }
}