using System.ComponentModel.DataAnnotations.Schema;
using Domain.ActivityLogs;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public Role Role { get; set; }
    public List<Token> CreatedTokens { get; set; }
    public List<Token> OwnedTokens { get; set; }
    public List<TokenCreationLog> TokenCreationLogs { get; set; }
    public List<AuctionParticipationLog> ParticipationLogs { get; set; }
    [InverseProperty("Seller")] public List<AuctionFinishLog> SellLogs { get; set; }
    [InverseProperty("Winner")] public List<AuctionFinishLog> BuyLogs { get; set; }
    public string ProfilePicUrl { get; set; } = "default_pfp.jpg";
    public string? Wallet { get; set; }
    public string? Seed { get; set; }
    public decimal Balance { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Description { get; set; } = "";
}