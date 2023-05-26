using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey("Id")]
public class Auction
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Token Token { get; set; }
    public DateTime Start { get; set; }
    public DateTime Finish { get; set; }
    public decimal StartPrice { get; set; }
    public User? Winner { get; set; }
    public decimal BestPrice { get; set; }
    public List<Bid> Bids { get; set; } = new();

    public Auction()
    {
    }

    public Auction(Token token, DateTime start, TimeSpan duration, decimal startPrice)
    {
        Token = token;
        Start = DateTime.SpecifyKind(start, DateTimeKind.Utc);
        Finish = Start + duration;
        StartPrice = startPrice;
    }
}