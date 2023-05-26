namespace Contracts.BackToFront.Full.Entities;

public class AuthorizedUserFull : UserFull
{
    public string Email { get; set; }
    public string Seed { get; set; }
    public decimal Balance { get; set; }
    public string Description { get; set; }
}