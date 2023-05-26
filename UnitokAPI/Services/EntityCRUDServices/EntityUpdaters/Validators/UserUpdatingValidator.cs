using Contracts.InterService.EntityValidationResults.Update;
using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Accessors.Entities;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters.Validators;

namespace Services.EntityCRUDServices.EntityUpdaters.Validators;

public class UserUpdatingValidator : IUserUpdatingValidator
{
    private readonly IDbUserAccessor _dbUserAccessor;

    public UserUpdatingValidator(IDbUserAccessor dbUserAccessor)
    {
        _dbUserAccessor = dbUserAccessor;
    }

    public async Task<UserBalanceUpdateValidationResult> ValidateBalanceUpdate(string userId, decimal newBalance)
    {
        var user = await _dbUserAccessor.GetById(userId);
        return user == null
            ? new UserBalanceUpdateValidationResult { Code = UserBalanceUpdateValidationCode.NoSuchUser }
            : new UserBalanceUpdateValidationResult { Code = UserBalanceUpdateValidationCode.Success, User = user };
    }
}