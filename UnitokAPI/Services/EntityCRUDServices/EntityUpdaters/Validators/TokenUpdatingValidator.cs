using Contracts.InterService.EntityValidationResults.Update;
using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Accessors.Entities;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters.Validators;

namespace Services.EntityCRUDServices.EntityUpdaters.Validators;

public class TokenUpdatingValidator : ITokenUpdatingValidator
{
    private readonly IDbUserAccessor _userAccessor;
    private readonly IDbTokenAccessor _tokenAccessor;

    public TokenUpdatingValidator(IDbUserAccessor userAccessor, IDbTokenAccessor tokenAccessor)
    {
        _userAccessor = userAccessor;
        _tokenAccessor = tokenAccessor;
    }

    public async Task<TokenOwnerChangeValidationResult> ValidateOwnerChange(string tokenId, string newOwnerId)
    {
        var token = await _tokenAccessor.GetById(tokenId);
        if (token == null)
            return new TokenOwnerChangeValidationResult { Code = TokenOwnerChangeValidationCode.NoSuchToken };
        var newOwner = await _userAccessor.GetById(newOwnerId);
        if (newOwner == null)
            return new TokenOwnerChangeValidationResult { Code = TokenOwnerChangeValidationCode.NoSuchUser };
        return new TokenOwnerChangeValidationResult
        {
            Code = TokenOwnerChangeValidationCode.Success,
            NewOwner = newOwner,
            OldOwner = token.Owner,
            Token = token
        };
    }
}