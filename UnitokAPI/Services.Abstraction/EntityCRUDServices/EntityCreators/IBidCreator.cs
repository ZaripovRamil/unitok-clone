using Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;
using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;

namespace Services.Abstraction.EntityCRUDServices.EntityCreators;

public interface IBidCreator
{
    public Task<BidCreationResult> Create(BidCreationData data);
}