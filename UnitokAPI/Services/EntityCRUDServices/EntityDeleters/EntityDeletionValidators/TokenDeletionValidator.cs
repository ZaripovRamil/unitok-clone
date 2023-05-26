using Contracts.InterService.EntityValidationResults.Deletion;
using Contracts.InterService.EntityValidationResults.Deletion.Codes;
using Domain.Accessors.Entities;
using Services.Abstraction.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

namespace Services.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

public class TokenDeletionValidator:ITokenDeletionValidator
{
    private readonly IDbTokenAccessor _tokenAccessor;

    public TokenDeletionValidator(IDbTokenAccessor tokenAccessor)
    {
        _tokenAccessor = tokenAccessor;
    }

    public async Task<TokenDeletionValidationResult> Validate(string id)
    {
        var token = await _tokenAccessor.GetById(id);
        return token == null ? 
            new TokenDeletionValidationResult(EntityDeletionValidationCode.NoSuchItem, null) 
            : new TokenDeletionValidationResult(EntityDeletionValidationCode.Success, token);
    }
}