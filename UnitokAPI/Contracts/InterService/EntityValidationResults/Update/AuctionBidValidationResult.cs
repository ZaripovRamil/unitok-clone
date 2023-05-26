using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Entities;

namespace Contracts.InterService.EntityValidationResults.Update;

public class AuctionBidValidationResult
{
    public BidAdditionValidationCode Code { get; set; }
    public User Bidder { get; set; }
    public Auction Auction { get; set; }
    public decimal Price { get; set; }
    public Bid Bid { get; set; }
}