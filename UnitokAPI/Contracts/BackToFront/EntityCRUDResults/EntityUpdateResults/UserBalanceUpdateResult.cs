using Contracts.InterService.EntityValidationResults.Update.Codes;

namespace Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;

public class UserBalanceUpdateResult
{
    private static readonly Dictionary<UserBalanceUpdateValidationCode, string> Messages =
        new()
        {
            { UserBalanceUpdateValidationCode.Success, "Success" },
            { UserBalanceUpdateValidationCode.NoSuchUser, "No such user" },
        };

    public UserBalanceUpdateResult(UserBalanceUpdateValidationCode code)
    {
        IsSuccessful = code == UserBalanceUpdateValidationCode.Success;
        ResultMessage = Messages[code];
    }

    public bool IsSuccessful { get; set; }
    public string ResultMessage { get; set; }
}