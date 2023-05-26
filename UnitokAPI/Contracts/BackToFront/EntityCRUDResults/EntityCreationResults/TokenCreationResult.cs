using Contracts.InterService.EntityValidationResults.Creation.Codes;

namespace Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;

public class TokenCreationResult:EntityCreationResult
{
    private static readonly Dictionary<TokenCreationValidationCode, string> Messages = new() {
        {TokenCreationValidationCode.Successful, "Success"},
        {TokenCreationValidationCode.InvalidAuthor, "Invalid AuthorId"},
        {TokenCreationValidationCode.EmptyName, "Name can't be empty"},
        {TokenCreationValidationCode.UnknownError, "Unknown error"}
    };
    public string? TokenId { get; set; }
    public TokenCreationResult(TokenCreationValidationCode validationCode, string? tokenId)
    {
        ResultMessage = Messages[validationCode];
        IsSuccessful = validationCode == TokenCreationValidationCode.Successful;
        TokenId = tokenId;
    }
}