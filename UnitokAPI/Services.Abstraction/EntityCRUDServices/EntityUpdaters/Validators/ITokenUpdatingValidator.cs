using Contracts.InterService.EntityValidationResults.Update;

namespace Services.Abstraction.EntityCRUDServices.EntityUpdaters.Validators;

public interface ITokenUpdatingValidator
{
    public Task<TokenOwnerChangeValidationResult> ValidateOwnerChange(string tokenId, string newOwnerId);
}