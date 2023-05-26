using Contracts.BackToFront.Light;
using Contracts.BackToFront.Light.Entities;

namespace Contracts.BackToFront.LogEntries;

public class AuctionFinishLogDto:ActivityLogDto
{
    public AuctionLight Auction { get; set; }
    public UserLight Winner { get; set; }
    public TokenLight Token { get; set; }
    public UserLight Seller { get; set; }
    public DateTime FinishTime { get; set; }
    public decimal Price { get; set; }
}