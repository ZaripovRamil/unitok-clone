using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Entities;

namespace Contracts.InterService.EntityValidationResults.Update;

public class TokenOwnerChangeValidationResult
{
    public TokenOwnerChangeValidationCode Code { get; set; }
    public Token Token { get; set; }
    public User OldOwner { get; set; }
    public User NewOwner { get; set; }
}