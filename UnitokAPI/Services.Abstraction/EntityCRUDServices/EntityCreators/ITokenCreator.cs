using Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;
using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;

namespace Services.Abstraction.EntityCRUDServices.EntityCreators;

public interface ITokenCreator
{
    public Task<TokenCreationResult> Create(TokenCreationData data);
}