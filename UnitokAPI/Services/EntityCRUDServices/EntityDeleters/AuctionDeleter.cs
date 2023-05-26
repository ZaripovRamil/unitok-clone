using Contracts.BackToFront.EntityCRUDResults.EntityDeletionResults;
using Domain.Accessors.Entities;
using Services.Abstraction.EntityCRUDServices.EntityDeleters;
using Services.Abstraction.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

namespace Services.EntityCRUDServices.EntityDeleters;

public class AuctionDeleter:IAuctionDeleter
{

    private readonly IDbAuctionAccessor _auctionAccessor;
    private readonly IAuctionDeletionValidator _validator;

    public AuctionDeleter(IDbAuctionAccessor auctionAccessor, IAuctionDeletionValidator validator)
    {
        _auctionAccessor = auctionAccessor;
        _validator = validator;
    }

    public async Task<EntityDeletionResult> Delete(string id)
    {
        var validationResult =await _validator.Validate(id);
        if(validationResult.IsValid)
            await _auctionAccessor.Delete(validationResult.Auction);
        return new EntityDeletionResult(validationResult.ValidationCode);
    }
}