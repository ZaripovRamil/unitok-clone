using Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;
using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Domain.Accessors.Entities;
using Domain.Entities;
using Services.Abstraction.EntityCRUDServices.EntityCreators;
using Services.Abstraction.EntityCRUDServices.EntityCreators.EntityCreationValidators;

namespace Services.EntityCRUDServices.EntityCreators;

public class BidCreator : IBidCreator
{
    private readonly IDbBidAccessor _bidAccessor;
    private readonly IBidCreationValidator _validator;

    public BidCreator(IDbBidAccessor bidAccessor, IBidCreationValidator validator)
    {
        _bidAccessor = bidAccessor;
        _validator = validator;
    }

    public async Task<BidCreationResult> Create(BidCreationData data)
    {
        var validationResult = await _validator.Validate(data);
        if (!validationResult.IsValid)
            return new BidCreationResult((BidCreationValidationCode)validationResult.ValidationCode, null);
        var bid = new Bid(validationResult.Auction, data.Price, validationResult.Bidder, DateTime.Now);
        await _bidAccessor.Add(bid);
        return new BidCreationResult((BidCreationValidationCode)validationResult.ValidationCode, bid.Id);
    }
}