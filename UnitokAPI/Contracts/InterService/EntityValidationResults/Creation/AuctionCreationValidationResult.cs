using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Domain.Entities;

namespace Contracts.InterService.EntityValidationResults.Creation;

public class AuctionCreationValidationResult:EntityCreationValidationResult
{
    public AuctionCreationValidationResult(AuctionCreationValidationCode validationCode, Token token) : base((int)validationCode)
    {
        Token = token;
    }
    
    public Token Token { get; set; }
}