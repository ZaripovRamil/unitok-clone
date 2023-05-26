using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey("Id")]
public class Bid
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Auction Auction { get; set; }
    public decimal Price { get; set; }
    public User Bidder { get; set; }
    public DateTime BidTime { get; set; }
    
    public Bid() { }
    
    public Bid(Auction auction, decimal price, User bidder, DateTime bidTime)
    {
        Auction = auction;
        Price = price;
        Bidder = bidder;
        BidTime = DateTime.SpecifyKind(bidTime, DateTimeKind.Utc);
    }
}