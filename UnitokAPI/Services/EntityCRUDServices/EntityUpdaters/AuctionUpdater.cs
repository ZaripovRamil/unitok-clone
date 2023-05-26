using Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;
using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Accessors.Entities;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters.Validators;

namespace Services.EntityCRUDServices.EntityUpdaters;

public class AuctionUpdater : IAuctionUpdater
{
    private readonly IAuctionUpdatingValidator _updatingValidator;
    private readonly IDbAuctionAccessor _dbAuctionAccessor;

    public AuctionUpdater(IAuctionUpdatingValidator updatingValidator,
        IDbAuctionAccessor dbAuctionAccessor)
    {
        _updatingValidator = updatingValidator;
        _dbAuctionAccessor = dbAuctionAccessor;
    }

    public async Task<AuctionBidAdditionResult> AddBid(string auctionId, string userId, decimal price)
    {
        var validationResult = await _updatingValidator.ValidateBid(auctionId, userId, price);
        if (validationResult.Code != BidAdditionValidationCode.Success)
            return new AuctionBidAdditionResult(validationResult.Code);
        validationResult.Auction.Bids.Add(validationResult.Bid);
        await _dbAuctionAccessor.SaveChangesAsync();
        await UpdateBestPrice(auctionId, price);
        return new AuctionBidAdditionResult(validationResult.Code);
    }

    public async Task UpdateBestPrice(string auctionId, decimal newPrice)
    {
        var auction = await _dbAuctionAccessor.GetById(auctionId);
        auction!.BestPrice = Math.Max(auction.BestPrice, newPrice);
        await _dbAuctionAccessor.SaveChangesAsync();
    }

    public async Task Finish(string auctionId)
    {
        var auction = await _dbAuctionAccessor.GetById(auctionId);
        auction!.Winner = auction.Bids.Count > 0 ? auction.Bids.MaxBy(b => b.Price)?.Bidder : auction.Token.Owner;
        await _dbAuctionAccessor.SaveChangesAsync();
    }
}