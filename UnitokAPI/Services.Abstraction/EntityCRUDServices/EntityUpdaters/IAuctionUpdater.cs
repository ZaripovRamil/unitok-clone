using Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;

namespace Services.Abstraction.EntityCRUDServices.EntityUpdaters;

public interface IAuctionUpdater
{
    public Task<AuctionBidAdditionResult> AddBid(string auctionId, string userId, decimal price);
    public Task Finish(string auctionId);
}