using Contracts.InterService.EntityValidationResults.Deletion;

namespace Services.Abstraction.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

public interface IBidDeletionValidator
{
    public Task<BidDeletionValidationResult> Validate(string id);
}