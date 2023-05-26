using Contracts.InterService.EntityValidationResults.Deletion.Codes;
using Domain.Entities;

namespace Contracts.InterService.EntityValidationResults.Deletion;

public class BidDeletionValidationResult:EntityDeletionValidationResult
{
    public BidDeletionValidationResult(EntityDeletionValidationCode validationCode, Bid? bid) : base(validationCode)
    {
        Bid = bid;
    }
    
    public Bid? Bid { get; set; }
}