namespace UI.Models;

public class ChangePasswordVm
{
    public string? UserName { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
}