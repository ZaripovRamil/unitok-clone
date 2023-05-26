using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Domain.Entities;

namespace Contracts.InterService.EntityValidationResults.Creation;

public class BidCreationValidationResult:EntityCreationValidationResult
{
    public BidCreationValidationResult(BidCreationValidationCode validationCode, Auction? auction, User? bidder) : base((int)validationCode)
    {
        Auction = auction;
        Bidder = bidder;
    }
    
    public Auction? Auction { get; set; }
    public User? Bidder { get; set; }
}