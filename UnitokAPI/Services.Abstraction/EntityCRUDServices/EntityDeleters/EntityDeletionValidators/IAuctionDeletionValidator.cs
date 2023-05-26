using Contracts.InterService.EntityValidationResults.Deletion;

namespace Services.Abstraction.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

public interface IAuctionDeletionValidator
{
    public Task<AuctionDeletionValidationResult> Validate(string id);
}