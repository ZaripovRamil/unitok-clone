using Contracts.InterService.EntityValidationResults.Deletion.Codes;
using Domain.Entities;

namespace Contracts.InterService.EntityValidationResults.Deletion;

public class TokenDeletionValidationResult:EntityDeletionValidationResult
{
    public TokenDeletionValidationResult(EntityDeletionValidationCode validationCode, Token token) : base(validationCode)
    {
        Token = token;
    }
    
    public Token Token { get; set; }
}