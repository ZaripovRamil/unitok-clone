using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Creation;
using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Domain.Accessors.Entities;
using Domain.Entities;
using Services.Abstraction.EntityCRUDServices.EntityCreators.EntityCreationValidators;

namespace Services.EntityCRUDServices.EntityCreators.EntityCreationValidators;

public class BidCreationValidator:IBidCreationValidator
{
    private readonly IDbAuctionAccessor _auctionAccessor;
    private readonly IDbUserAccessor _userAccessor;

    public BidCreationValidator(IDbAuctionAccessor auctionAccessor, IDbUserAccessor userAccessor)
    {
        _auctionAccessor = auctionAccessor;
        _userAccessor = userAccessor;
    }

    public async Task<BidCreationValidationResult> Validate(BidCreationData data)
    {
        Auction? auction = null;
        User? bidder = null;
        var state = BidCreationValidationCode.Successful;
        if (data.Price < 0) state = BidCreationValidationCode.NegativePrice;
        else
        {
            auction = await _auctionAccessor.GetById(data.AuctionId);
            if (auction == null) state = BidCreationValidationCode.InvalidAuction;
            else
            {
                bidder = await _userAccessor.GetById(data.BidderId);
                if (bidder == null) state = BidCreationValidationCode.InvalidBidder;
            }
        }

        return new BidCreationValidationResult(state, auction, bidder);
    }
}