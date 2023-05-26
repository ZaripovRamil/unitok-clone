using Contracts.InterService.EntityValidationResults.Update;

namespace Services.Abstraction.EntityCRUDServices.EntityUpdaters.Validators;

public interface IAuctionUpdatingValidator
{
    public Task<AuctionBidValidationResult> ValidateBid(string auctionId, string userId, decimal price);
}