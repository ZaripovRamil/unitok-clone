using Contracts.BackToFront.EntityCRUDResults.EntityDeletionResults;

namespace Services.Abstraction.EntityCRUDServices.EntityDeleters;

public interface ITokenDeleter
{
    public Task<EntityDeletionResult> Delete(string id);
}