using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Creation;

namespace Services.Abstraction.EntityCRUDServices.EntityCreators.EntityCreationValidators;

public interface IBidCreationValidator
{
    public Task<BidCreationValidationResult> Validate(BidCreationData data);
}