using Contracts.InterService.EntityValidationResults.Creation.Codes;

namespace Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;

public class BidCreationResult:EntityCreationResult
{
    private static readonly Dictionary<BidCreationValidationCode, string> Messages = new() {
        {BidCreationValidationCode.Successful, "Success"},
        {BidCreationValidationCode.InvalidBidder, "Invalid BidderId"},
        {BidCreationValidationCode.InvalidAuction, "Invalid AuctionId"},
        {BidCreationValidationCode.NegativePrice,"Price can't be negative"},
        {BidCreationValidationCode.UnknownError, "Unknown error"}
    };
    public string? BidId { get; set; }

    public BidCreationResult(BidCreationValidationCode validationCode, string? bidId)
    {
        ResultMessage = Messages[validationCode];
        IsSuccessful = validationCode == BidCreationValidationCode.Successful;
        BidId = bidId;
    }
}