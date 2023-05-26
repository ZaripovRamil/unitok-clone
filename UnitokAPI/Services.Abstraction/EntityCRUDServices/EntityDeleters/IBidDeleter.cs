using Contracts.BackToFront.EntityCRUDResults.EntityDeletionResults;

namespace Services.Abstraction.EntityCRUDServices.EntityDeleters;

public interface IBidDeleter
{
    public Task<EntityDeletionResult> Delete(string id);
}