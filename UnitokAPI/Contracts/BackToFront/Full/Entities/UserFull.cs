using Contracts.BackToFront.Light;
using Contracts.BackToFront.Light.Entities;
using Contracts.BackToFront.LogEntries;

namespace Contracts.BackToFront.Full.Entities;

public class UserFull
{
    public string Id { get; set; }
    public string? UserName { get; set; }
    public string ProfilePicUrl { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<TokenLight> OwnedTokens { get; set; }
    public List<TokenLight> CreatedTokens { get; set; }
    public List<TokenCreationLogDto> TokenCreationLogs { get; set; }
    public List<AuctionFinishLogDto> SellLogs { get; set; }
    public List<AuctionFinishLogDto> BuyLogs { get; set; }
    public List<AuctionParticipanceLogDto> ParticipanceLogs { get; set; }
    public string Wallet { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Description { get; set; }
    public decimal Balance { get; set; }
}