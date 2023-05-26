using Contracts.InterService.EntityValidationResults.Creation.Codes;

namespace Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;

public class AuctionCreationResult:EntityCreationResult
{
    private static readonly Dictionary<AuctionCreationValidationCode, string> Messages = new() {
        {AuctionCreationValidationCode.Successful, "Success"},
        {AuctionCreationValidationCode.InvalidToken, "Invalid TokenId"},
        {AuctionCreationValidationCode.InvalidStartPrice, "Invalid start price"},
        {AuctionCreationValidationCode.InvalidDuration,"Invalid Duration"},
        {AuctionCreationValidationCode.UnknownError, "Unknown error"}
    };

    public AuctionCreationResult(AuctionCreationValidationCode validationCode, string? auctionId)
    {
        ResultMessage = Messages[validationCode];
        IsSuccessful = validationCode == AuctionCreationValidationCode.Successful;
        AuctionId = auctionId;
    }

    public string? AuctionId { get; set; }
}