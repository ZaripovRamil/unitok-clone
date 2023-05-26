using Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;
using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Accessors.Entities;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters.Validators;

namespace Services.EntityCRUDServices.EntityUpdaters;

public class TokenUpdater : ITokenUpdater
{
    private readonly ITokenUpdatingValidator _validator;
    private readonly IDbAccessor _dbAccessor;

    public TokenUpdater(ITokenUpdatingValidator validator, IDbAccessor dbAccessor)
    {
        _validator = validator;
        _dbAccessor = dbAccessor;
    }

    public async Task<TokenOwnerChangeResult> ChangeOwner(string tokenId, string newOwnerId)
    {
        var validationResult = await _validator.ValidateOwnerChange(tokenId, newOwnerId);
        if (validationResult.Code != TokenOwnerChangeValidationCode.Success)
            return new TokenOwnerChangeResult(validationResult.Code);
        validationResult.Token.Owner = validationResult.NewOwner;
        await _dbAccessor.SaveChangesAsync();
        return new TokenOwnerChangeResult(validationResult.Code);
    }
}