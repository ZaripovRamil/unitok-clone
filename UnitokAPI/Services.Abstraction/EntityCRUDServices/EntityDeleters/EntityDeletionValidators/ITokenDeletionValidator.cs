using Contracts.InterService.EntityValidationResults.Deletion;

namespace Services.Abstraction.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

public interface ITokenDeletionValidator
{
    public Task<TokenDeletionValidationResult> Validate(string id);
}