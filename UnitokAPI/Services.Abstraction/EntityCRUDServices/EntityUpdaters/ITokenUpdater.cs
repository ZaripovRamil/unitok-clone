using Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;

namespace Services.Abstraction.EntityCRUDServices.EntityUpdaters;

public interface ITokenUpdater
{
    public Task<TokenOwnerChangeResult> ChangeOwner(string tokenId, string newOwnerId);
}