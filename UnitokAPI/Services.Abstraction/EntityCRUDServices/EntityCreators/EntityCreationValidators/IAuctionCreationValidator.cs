using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Creation;

namespace Services.Abstraction.EntityCRUDServices.EntityCreators.EntityCreationValidators;

public interface IAuctionCreationValidator
{
    public Task<AuctionCreationValidationResult> Validate(AuctionCreationData data);
}