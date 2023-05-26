using Contracts.BackToFront.Light;
using Contracts.BackToFront.Light.Entities;
using Contracts.BackToFront.LogEntries;

namespace UI.Models;

public class UserProfileVM
{
    public string Email { get; set; }
    public string? Nickname { get; set; }
    public string ProfilePicUrl { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<TokenLight> OwnedTokens { get; set; }
    public List<TokenLight> CreatedTokens { get; set; }
    public List<AuctionLight> OnAuction { get; set; }
    public string Wallet { get; set; }
    public decimal Balance { get; set; }
    public bool IsAuthorized { get; set; }
    public IEnumerable<AuctionFinishLogDto> AuctionFinishLogs { get; set; }
    public IEnumerable<AuctionParticipanceLogDto> AuctionParticipationLogs { get; set; }
    public IEnumerable<TokenCreationLogDto> TokenCreationLogs { get; set; }
}