using Contracts.BackToFront.EntityCRUDResults.EntityDeletionResults;
using Domain.Accessors.Entities;
using Services.Abstraction.EntityCRUDServices.EntityDeleters;
using Services.Abstraction.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

namespace Services.EntityCRUDServices.EntityDeleters;

public class BidDeleter:IBidDeleter
{
    private readonly IDbBidAccessor _bidAccessor;
    private readonly IBidDeletionValidator _validator;

    public BidDeleter(IDbBidAccessor bidAccessor, IBidDeletionValidator validator)
    {
        _bidAccessor = bidAccessor;
        _validator = validator;
    }

    public async Task<EntityDeletionResult> Delete(string id)
    {
        var validationResult =await _validator.Validate(id);
        if (validationResult.IsValid)
            await _bidAccessor.Delete(validationResult.Bid);
        return new EntityDeletionResult(validationResult.ValidationCode);
    }
}