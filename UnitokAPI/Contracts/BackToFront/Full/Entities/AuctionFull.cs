using Contracts.BackToFront.Light;
using Contracts.BackToFront.Light.Entities;

namespace Contracts.BackToFront.Full.Entities;

public class AuctionFull
{
    public string Id { get; set; }
    public TokenLight Token { get; set; }
    public DateTime Start { get; set; }
    public DateTime Finish { get; set; }
    public decimal StartPrice { get; set; }
    public decimal BestPrice { get; set; }
    public UserLight? Winner { get; set; }
    public List<BidLight> Bids { get; set; }
}