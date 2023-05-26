namespace Models.Dto.FrontToBack.ChangeData;

public class ChangePasswordData
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}