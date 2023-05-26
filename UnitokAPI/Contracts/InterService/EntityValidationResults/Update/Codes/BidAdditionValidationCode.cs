namespace Contracts.InterService.EntityValidationResults.Update.Codes;

public enum BidAdditionValidationCode
{
    Success,
    NoSuchUser,
    NoSuchAuction,
    SmallPrice,
    LowBalance,
    AuctionFinished
}