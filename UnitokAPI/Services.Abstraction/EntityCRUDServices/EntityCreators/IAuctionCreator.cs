using Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;
using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;

namespace Services.Abstraction.EntityCRUDServices.EntityCreators;

public interface IAuctionCreator
{
    public Task<AuctionCreationResult> Create(AuctionCreationData data);
}