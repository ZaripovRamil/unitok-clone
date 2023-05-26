using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Creation;
using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Domain.Accessors.Entities;
using Domain.Entities;
using Services.Abstraction.EntityCRUDServices.EntityCreators.EntityCreationValidators;

namespace Services.EntityCRUDServices.EntityCreators.EntityCreationValidators;

public class AuctionCreationValidator:IAuctionCreationValidator
{

    private readonly IDbTokenAccessor _tokenAccessor;

    public AuctionCreationValidator(IDbTokenAccessor tokenAccessor)
    {
        _tokenAccessor = tokenAccessor;
    }

    public async Task<AuctionCreationValidationResult> Validate(AuctionCreationData data)
    {
        Token? token = null;
        var state = AuctionCreationValidationCode.Successful;
        if (data.Duration <= TimeSpan.Zero) state = AuctionCreationValidationCode.InvalidDuration;
        else if (data.StartPrice < 0) state = AuctionCreationValidationCode.InvalidStartPrice;
        else
        {
            token = await _tokenAccessor.GetById(data.TokenId);
            if (token == null) state = AuctionCreationValidationCode.InvalidToken;
        }
        return new AuctionCreationValidationResult(state, token);
    }
}