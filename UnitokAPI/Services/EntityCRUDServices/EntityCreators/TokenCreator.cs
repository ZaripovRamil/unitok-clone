using Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;
using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Domain.Accessors.Entities;
using Domain.Accessors.EntityOperations;
using Domain.ActivityLogs;
using Domain.Entities;
using Services.Abstraction.EntityCRUDServices.EntityCreators;
using Services.Abstraction.EntityCRUDServices.EntityCreators.EntityCreationValidators;

namespace Services.EntityCRUDServices.EntityCreators;

public class TokenCreator:ITokenCreator
{
    private readonly ITokenCreationValidator _validator;
    private readonly IDbTokenAccessor _tokenAccessor;
    private readonly IDbTokenCreationLogAccessor _tokenCreationLogAccessor;

    public TokenCreator(ITokenCreationValidator validator, IDbTokenAccessor tokenAccessor, IDbTokenCreationLogAccessor tokenCreationLogAccessor)
    {
        _validator = validator;
        _tokenAccessor = tokenAccessor;
        _tokenCreationLogAccessor = tokenCreationLogAccessor;
    }

    public async Task<TokenCreationResult> Create(TokenCreationData data)
    {
        var validationResult = await _validator.Validate(data);
        if (!validationResult.IsValid)
            return new TokenCreationResult((TokenCreationValidationCode)validationResult.ValidationCode, null);
        var token = new Token(data.Name, data.Type, data.Description, validationResult.Owner);
        await _tokenAccessor.Add(token);
        await _tokenCreationLogAccessor.Add(new TokenCreationLog(token, validationResult.Owner, token.CreatedAt));
        return new TokenCreationResult((TokenCreationValidationCode)validationResult.ValidationCode, token.Id);
    }
}