namespace Contracts.FrontToBack.EntityCRUDData.EntityUpdateData;

public class ChangePasswordData
{
    public string Id { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ComfirmPassword { get; set; }
}