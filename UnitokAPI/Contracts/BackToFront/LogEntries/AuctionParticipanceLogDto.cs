using Contracts.BackToFront.Light;
using Contracts.BackToFront.Light.Entities;

namespace Contracts.BackToFront.LogEntries;

public class AuctionParticipanceLogDto : ActivityLogDto
{
    public AuctionLight Auction { get; set; }
    public TokenLight Token { get; set; }
    public UserLight Bidder { get; set; }
    public bool HasWon { get; set; }
}