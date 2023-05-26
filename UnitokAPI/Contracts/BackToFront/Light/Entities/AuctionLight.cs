namespace Contracts.BackToFront.Light.Entities;

public class AuctionLight
{
    public string Id { get; set; }
    public TokenLight Token { get; set; }
    public DateTime Start { get; set; }
    public DateTime Finish { get; set; }
    public decimal Price { get; set; }
}