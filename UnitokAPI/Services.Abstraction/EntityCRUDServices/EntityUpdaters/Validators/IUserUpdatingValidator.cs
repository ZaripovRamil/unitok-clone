using Contracts.InterService.EntityValidationResults.Update;

namespace Services.Abstraction.EntityCRUDServices.EntityUpdaters.Validators;

public interface IUserUpdatingValidator
{
    public Task<UserBalanceUpdateValidationResult> ValidateBalanceUpdate(string userId, decimal newBalance);
}