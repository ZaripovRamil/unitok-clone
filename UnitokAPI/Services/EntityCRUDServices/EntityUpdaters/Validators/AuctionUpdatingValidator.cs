using Contracts.InterService.EntityValidationResults.Update;
using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Accessors.Entities;
using Domain.Entities;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters.Validators;

namespace Services.EntityCRUDServices.EntityUpdaters.Validators;

public class AuctionUpdatingValidator : IAuctionUpdatingValidator
{
    private readonly IDbAuctionAccessor _auctionAccessor;
    private readonly IDbUserAccessor _userAccessor;
    private readonly IDbBidAccessor _bidAccessor;

    public AuctionUpdatingValidator(IDbAuctionAccessor auctionAccessor, IDbUserAccessor userAccessor,
        IDbBidAccessor bidAccessor)
    {
        _auctionAccessor = auctionAccessor;
        _userAccessor = userAccessor;
        _bidAccessor = bidAccessor;
    }

    public async Task<AuctionBidValidationResult> ValidateBid(string auctionId, string userId, decimal price)
    {
        var auction = await _auctionAccessor.GetById(auctionId);
        if (auction == null) return new AuctionBidValidationResult { Code = BidAdditionValidationCode.NoSuchAuction };
        var user = await _userAccessor.GetById(userId);
        if (user == null) return new AuctionBidValidationResult { Code = BidAdditionValidationCode.NoSuchUser };
        if (auction.Bids.Select(b => b.Price).Append(auction.StartPrice).Max() >= price)
            return new AuctionBidValidationResult { Code = BidAdditionValidationCode.SmallPrice };
        if (price > user.Balance) return new AuctionBidValidationResult { Code = BidAdditionValidationCode.LowBalance };
        var bid = new Bid(auction, price, user, DateTime.Now);
        await _bidAccessor.Add(bid);
        return new AuctionBidValidationResult
            { Code = BidAdditionValidationCode.Success, Auction = auction, Bidder = user, Price = price, Bid = bid };
    }
}