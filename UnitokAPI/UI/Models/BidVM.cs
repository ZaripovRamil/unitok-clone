namespace UI.Models;

public class BidVM
{
    public string Id { get; set; }
    public string BidderName { get; set; }
    public DateTime BidTime { get; set; }
    public decimal BidValue { get; set; }
    public string AvatarUrl { get; set; }
}