using Contracts.FrontToBack.EntityCRUDData.EntityUpdateData;
using Contracts.InterService.EntityValidationResults.Update;

namespace Services.Abstraction.EntityCRUDServices.EntityUpdaters.EntityUpdateValidators;

public interface IUserUpdateValidator
{
    public Task<UserUpdateValidationResult> Validate(ChangeBalanceData data);
    public Task<UserUpdateValidationResult> Validate(ChangePasswordData data);
    public Task<UserUpdateValidationResult> Validate(ChangeProfileDetailsData data);
}