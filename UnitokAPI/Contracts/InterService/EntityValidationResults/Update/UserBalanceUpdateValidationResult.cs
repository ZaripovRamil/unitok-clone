using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Entities;

namespace Contracts.InterService.EntityValidationResults.Update;

public class UserBalanceUpdateValidationResult
{
    public UserBalanceUpdateValidationCode Code { get; set; }
    public User User { get; set; }
}