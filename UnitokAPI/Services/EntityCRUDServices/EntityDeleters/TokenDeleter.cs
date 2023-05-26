using Contracts.BackToFront.EntityCRUDResults.EntityDeletionResults;
using Domain.Accessors.Entities;
using Services.Abstraction.EntityCRUDServices.EntityDeleters;
using Services.Abstraction.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

namespace Services.EntityCRUDServices.EntityDeleters;

public class TokenDeleter:ITokenDeleter
{
    private readonly IDbTokenAccessor _tokenAccessor;
    private readonly ITokenDeletionValidator _validator;

    public TokenDeleter(IDbTokenAccessor tokenAccessor, ITokenDeletionValidator validator)
    {
        _tokenAccessor = tokenAccessor;
        _validator = validator;
    }

    public async Task<EntityDeletionResult> Delete(string id)
    {
        var validationResult =await _validator.Validate(id);
        if (validationResult.IsValid)
            await _tokenAccessor.Delete(validationResult.Token);
        return new EntityDeletionResult(validationResult.ValidationCode);
    }
}