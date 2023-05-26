using Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;
using Contracts.FrontToBack.EntityCRUDData.EntityUpdateData;
using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Accessors.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters.EntityUpdateValidators;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters.Validators;

namespace Services.EntityCRUDServices.EntityUpdaters;

public class UserUpdater : IUserUpdater
{
    private readonly IDbUserAccessor _userAccessor;
    private readonly IUserUpdateValidator _validator;
    private readonly IUserUpdatingValidator _validator1;
    private readonly UserManager<User> _userManager;
    private readonly IDbAccessor _dbAccessor;

    public UserUpdater(IDbUserAccessor auctionAccessor, IUserUpdateValidator validator, UserManager<User> userManager, IDbAccessor dbAccessor, IUserUpdatingValidator validator1)
    {
        _userAccessor = auctionAccessor;
        _validator = validator;
        _userManager = userManager;
        _dbAccessor = dbAccessor;
        _validator1 = validator1;
    }


    public async Task<UserBalanceUpdateResult> UpdateBalance(string userId, decimal newBalance)
    {
        var validationResult = await _validator1.ValidateBalanceUpdate(userId, newBalance);
        if (validationResult.Code != UserBalanceUpdateValidationCode.Success)
            return new UserBalanceUpdateResult(validationResult.Code);
        validationResult.User.Balance = newBalance;
        await _dbAccessor.SaveChangesAsync();
        return new UserBalanceUpdateResult(validationResult.Code);
    }
    
    public async Task<UserUpdateResult> Update(ChangeBalanceData data)
    {
        var validationResult = await _validator.Validate(data);
        if (!validationResult.IsValid)
            return new UserUpdateResult((UserUpdateValidationCode)validationResult.ValidationCode,
                null);
        await _userAccessor.Update(validationResult.User);

        return new UserUpdateResult((UserUpdateValidationCode)validationResult.ValidationCode, validationResult.User.Id);
    }

    public async Task<UserUpdateResult> Update(ChangePasswordData data)
    {
        var validationResult = await _validator.Validate(data);
        if (!validationResult.IsValid)
            return new UserUpdateResult((UserUpdateValidationCode)validationResult.ValidationCode,
                null);
        await _userManager.ChangePasswordAsync(validationResult.User,data.OldPassword,data.NewPassword);
        return new UserUpdateResult((UserUpdateValidationCode)validationResult.ValidationCode, validationResult.User.Id);
    }

    public async Task<UserUpdateResult> Update(ChangeProfileDetailsData data)
    {
        var validationResult = await _validator.Validate(data);
        if (!validationResult.IsValid)
            return new UserUpdateResult((UserUpdateValidationCode)validationResult.ValidationCode,
                null);
        await _userAccessor.Update(validationResult.User);

        return new UserUpdateResult((UserUpdateValidationCode)validationResult.ValidationCode, validationResult.User.Id);
    }
}