namespace Contracts.InterService.EntityValidationResults.Creation.Codes;

public enum BidCreationValidationCode
{
    Successful = EntityCreationValidationCode.Successful,
    InvalidBidder,
    UnknownError,
    InvalidAuction,
    NegativePrice
}