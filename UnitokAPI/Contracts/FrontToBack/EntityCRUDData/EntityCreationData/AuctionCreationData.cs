namespace Contracts.FrontToBack.EntityCRUDData.EntityCreationData;

public class AuctionCreationData
{
    public string TokenId { get; set; }
    public decimal StartPrice { get; set; }
    public TimeSpan Duration { get; set; }
}