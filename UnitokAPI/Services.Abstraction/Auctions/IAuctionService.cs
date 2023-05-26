using Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;
using Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;
using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;

namespace Services.Abstraction.Auctions;

public interface IAuctionService
{
    public Task<AuctionBidAdditionResult> PlaceBidAsync(string auctionId, string userId, decimal bidValue);
    public Task<AuctionCreationResult> CreateAuctionAsync(AuctionCreationData creationData);
    public Task FinishAuctionAsync(string auctionId);
}