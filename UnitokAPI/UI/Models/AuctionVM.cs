namespace UI.Models;

public class AuctionVM
{
    public string Id { get; set; }
    public TokenVM Token { get; set; }
    public string CreatorName { get; set; }
    public string CreatorAvatarUrl { get; set; }
    public IEnumerable<BidVM> Bids { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime FinishTime { get; set; }
    public decimal CurrentBidPrice { get; set; }
    public IEnumerable<TokenCardVM> OwnerCards { get; set; }
}