using Contracts.InterService.EntityValidationResults.Update.Codes;

namespace Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;

public class AuctionBidAdditionResult
{
    private static readonly Dictionary<BidAdditionValidationCode, string> Messages =
        new()
        {
            { BidAdditionValidationCode.Success, "Success" },
            { BidAdditionValidationCode.NoSuchAuction, "No such auction" },
            { BidAdditionValidationCode.NoSuchUser, "No such user" },
            { BidAdditionValidationCode.SmallPrice, "Too small bid" },
            { BidAdditionValidationCode.LowBalance, "Not enough money on balance" },
            { BidAdditionValidationCode.AuctionFinished, "The auction has finished" }
        };

    public AuctionBidAdditionResult(BidAdditionValidationCode code)
    {
        IsSuccessful = code == BidAdditionValidationCode.Success;
        ResultMessage = Messages[code];
    }

    public AuctionBidAdditionResult() { }

    public bool IsSuccessful { get; set; }
    public string ResultMessage { get; set; }
}