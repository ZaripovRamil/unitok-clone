using Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;
using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Domain.Accessors.Entities;
using Domain.Entities;
using Services.Abstraction.EntityCRUDServices.EntityCreators;
using Services.Abstraction.EntityCRUDServices.EntityCreators.EntityCreationValidators;

namespace Services.EntityCRUDServices.EntityCreators;

public class AuctionCreator:IAuctionCreator
{
    private readonly IDbAuctionAccessor _auctionAccessor;
    private readonly IAuctionCreationValidator _validator;

    public AuctionCreator(IDbAuctionAccessor auctionAccessor, IAuctionCreationValidator validator)
    {
        _auctionAccessor = auctionAccessor;
        _validator = validator;
    }

    public async Task<AuctionCreationResult> Create(AuctionCreationData data)
    {
        var validationResult = await _validator.Validate(data);
        if (!validationResult.IsValid)
            return new AuctionCreationResult((AuctionCreationValidationCode)validationResult.ValidationCode,
                null);
        var auction = new Auction(validationResult.Token, DateTime.Now, data.Duration, data.StartPrice);
        await _auctionAccessor.Add(auction);

        return new AuctionCreationResult((AuctionCreationValidationCode)validationResult.ValidationCode, auction.Id);
    }
}