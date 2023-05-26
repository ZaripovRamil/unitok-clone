using Contracts.InterService.EntityValidationResults.Update.Codes;

namespace Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;

public class TokenOwnerChangeResult
{
    private static readonly Dictionary<TokenOwnerChangeValidationCode, string> Messages =
        new()
        {
            { TokenOwnerChangeValidationCode.Success, "Success" },
            { TokenOwnerChangeValidationCode.NoSuchToken, "No such token" },
            { TokenOwnerChangeValidationCode.NoSuchUser, "No such user" },
        };

    public TokenOwnerChangeResult(TokenOwnerChangeValidationCode code)
    {
        IsSuccessful = code == TokenOwnerChangeValidationCode.Success;
        ResultMessage = Messages[code];
    }

    public bool IsSuccessful { get; set; }
    public string ResultMessage { get; set; }
}