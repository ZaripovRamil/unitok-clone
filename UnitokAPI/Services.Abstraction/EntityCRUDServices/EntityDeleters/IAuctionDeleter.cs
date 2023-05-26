using Contracts.BackToFront.EntityCRUDResults.EntityDeletionResults;

namespace Services.Abstraction.EntityCRUDServices.EntityDeleters;

public interface IAuctionDeleter
{
    public Task<EntityDeletionResult> Delete(string id);
}