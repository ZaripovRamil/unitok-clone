using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Creation;

namespace Services.Abstraction.EntityCRUDServices.EntityCreators.EntityCreationValidators;

public interface ITokenCreationValidator
{
    public Task<TokenCreationValidationResult> Validate(TokenCreationData data);
}