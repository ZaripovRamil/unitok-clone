using Contracts.InterService.EntityValidationResults.Deletion;
using Contracts.InterService.EntityValidationResults.Deletion.Codes;
using Domain.Accessors.Entities;
using Services.Abstraction.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

namespace Services.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

public class BidDeletionValidator:IBidDeletionValidator
{
    private readonly IDbBidAccessor _bidAccessor;

    public BidDeletionValidator(IDbBidAccessor bidAccessor)
    {
        _bidAccessor = bidAccessor;
    }

    public async Task<BidDeletionValidationResult> Validate(string id)
    {
        var bid = await _bidAccessor.GetById(id);
        return bid == null ? 
            new BidDeletionValidationResult(EntityDeletionValidationCode.NoSuchItem, null) 
            : new BidDeletionValidationResult(EntityDeletionValidationCode.Success, bid);
    }
}