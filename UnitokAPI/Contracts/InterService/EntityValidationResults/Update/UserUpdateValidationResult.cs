using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Entities;

namespace Contracts.InterService.EntityValidationResults.Update;

public class UserUpdateValidationResult : EntityUpdateValidationResult
{
    public UserUpdateValidationResult(UserUpdateValidationCode validationCode, User user) : base((int)validationCode)
    {
        User = user;
    }
    public User User { get; set; }
}