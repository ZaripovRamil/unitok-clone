using Contracts.InterService.EntityValidationResults.Update.Codes;

namespace Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;

public class UserUpdateResult : EntityUpdateResult
{
    private static readonly Dictionary<UserUpdateValidationCode, string> Messages = new() {
        {UserUpdateValidationCode.Successful, "Success"},
        {UserUpdateValidationCode.InvalidSeedPhrase, "Invalid seed phrase"},
        {UserUpdateValidationCode.PasswordMismatch, "Password Mismatch"},
        {UserUpdateValidationCode.EmptyValue,"One of values is empty"},
        {UserUpdateValidationCode.NoSuchItem,"User not found"},
        {UserUpdateValidationCode.UnknownError, "Unknown error"}
    };
    public string? UserId { get; set; }

    public UserUpdateResult(UserUpdateValidationCode validationCode, string? userId)
    {
        ResultMessage = Messages[validationCode];
        IsSuccessful = validationCode == UserUpdateValidationCode.Successful;
        UserId = userId;
    }
}