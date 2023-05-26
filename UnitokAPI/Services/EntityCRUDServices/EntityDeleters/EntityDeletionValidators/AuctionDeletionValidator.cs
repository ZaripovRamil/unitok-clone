using Contracts.InterService.EntityValidationResults.Deletion;
using Contracts.InterService.EntityValidationResults.Deletion.Codes;
using Domain.Accessors.Entities;
using Services.Abstraction.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

namespace Services.EntityCRUDServices.EntityDeleters.EntityDeletionValidators;

public class AuctionDeletionValidator:IAuctionDeletionValidator
{
    private readonly IDbAuctionAccessor _auctionAccessor;

    public AuctionDeletionValidator(IDbAuctionAccessor auctionAccessor)
    {
        _auctionAccessor = auctionAccessor;
    }

    public async Task<AuctionDeletionValidationResult> Validate(string id)
    {
        var auction = await _auctionAccessor.GetById(id);
        return auction == null ? 
            new AuctionDeletionValidationResult(EntityDeletionValidationCode.NoSuchItem, null) 
            : new AuctionDeletionValidationResult(EntityDeletionValidationCode.Success, auction);
    }
}