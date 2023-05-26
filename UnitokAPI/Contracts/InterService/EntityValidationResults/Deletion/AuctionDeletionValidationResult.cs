using Contracts.InterService.EntityValidationResults.Deletion.Codes;
using Domain.Entities;

namespace Contracts.InterService.EntityValidationResults.Deletion;

public class AuctionDeletionValidationResult:EntityDeletionValidationResult
{
    public AuctionDeletionValidationResult(EntityDeletionValidationCode validationCode, Auction auction) : base(validationCode)
    {
        Auction = auction;
    }
    
    public Auction Auction { get; set; }
}