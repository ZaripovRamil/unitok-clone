using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Domain.Entities;

namespace Contracts.InterService.EntityValidationResults.Creation;

public class TokenCreationValidationResult:EntityCreationValidationResult
{
    public TokenCreationValidationResult(TokenCreationValidationCode validationCode, User? owner) : base((int)validationCode)
    {
        Owner = owner;
    }
    
    public User? Owner { get; set; }
}