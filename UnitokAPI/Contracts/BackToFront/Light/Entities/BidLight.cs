namespace Contracts.BackToFront.Light.Entities;

public class BidLight
{
    public string Id { get; set; }
    public decimal Price { get; set; }
    public UserLight Bidder { get; set; }
    public DateTime BidTime { get; set; }
}