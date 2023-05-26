using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.FrontToBack.EntityCRUDData.EntityUpdateData;
using Contracts.InterService.EntityValidationResults.Creation;
using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Contracts.InterService.EntityValidationResults.Update;
using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Accessors.Entities;
using Domain.Entities;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters.EntityUpdateValidators;

namespace Services.EntityCRUDServices.EntityUpdaters.EntityUpdateValidators;

public class UserUpdateValidator : IUserUpdateValidator
{
    private readonly IDbUserAccessor _tokenAccessor;

    public UserUpdateValidator(IDbUserAccessor tokenAccessor)
    {
        _tokenAccessor = tokenAccessor;
    }

    public async Task<UserUpdateValidationResult> Validate(ChangeBalanceData data)
    {
        var state = UserUpdateValidationCode.Successful;
        var user = await _tokenAccessor.GetById(data.Id);
        if (user == null) state = UserUpdateValidationCode.NoSuchItem;
        else if (data.Seed == "") state = UserUpdateValidationCode.InvalidSeedPhrase;
        user.Balance = data.Amount;
        return new UserUpdateValidationResult(state,user);
    }

    public async Task<UserUpdateValidationResult> Validate(ChangePasswordData data)
    {
        var state = UserUpdateValidationCode.Successful;
        var user = await _tokenAccessor.GetById(data.Id);
        if (user == null) state = UserUpdateValidationCode.NoSuchItem;
        else if (data.NewPassword != data.ComfirmPassword) state = UserUpdateValidationCode.PasswordMismatch;
        return new UserUpdateValidationResult(state,user);
    }

    public async Task<UserUpdateValidationResult> Validate(ChangeProfileDetailsData data)
    {
        var state = UserUpdateValidationCode.Successful;
        var user = await _tokenAccessor.GetById(data.Id);
        if (user == null) state = UserUpdateValidationCode.NoSuchItem;
        else if (data.FirstName == null || data.LastName == null || data.UserName == null) 
            state = UserUpdateValidationCode.EmptyValue;
        user.FirstName = data.FirstName;
        user.LastName = data.LastName;
        return new UserUpdateValidationResult(state, user);
    }
}