using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Creation;
using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Domain.Accessors.Entities;
using Domain.Entities;
using Services.Abstraction.EntityCRUDServices.EntityCreators.EntityCreationValidators;

namespace Services.EntityCRUDServices.EntityCreators.EntityCreationValidators;

public class TokenCreationValidator:ITokenCreationValidator
{
    private readonly IDbUserAccessor _userAccessor;

    public TokenCreationValidator(IDbUserAccessor userAccessor)
    {
        _userAccessor = userAccessor;
    }

    public async Task<TokenCreationValidationResult> Validate(TokenCreationData data)
    {
        User? owner = null;
        var state = TokenCreationValidationCode.Successful;
        if (data.Name == String.Empty) state = TokenCreationValidationCode.EmptyName;
        else
        {
            owner = await _userAccessor.GetById(data.CreatorId);
            if (owner == null) state = TokenCreationValidationCode.InvalidAuthor;
        }

        return new TokenCreationValidationResult(state, owner);
    }
}