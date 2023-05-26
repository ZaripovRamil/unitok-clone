using Contracts.BackToFront.Light.Entities;

namespace Contracts.BackToFront.Full.Entities;

public class BidFull
{
    public string Id { get; set; }
    public decimal Price { get; set; }
    public AuctionLight Auction { get; set; }
    public UserLight Bidder { get; set; }
    public DateTime BidTime { get; set; }
}