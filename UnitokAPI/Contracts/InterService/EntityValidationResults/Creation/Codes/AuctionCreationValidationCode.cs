namespace Contracts.InterService.EntityValidationResults.Creation.Codes;

public enum AuctionCreationValidationCode
{
    Successful = EntityCreationValidationCode.Successful,
    InvalidToken,
    InvalidStartPrice,
    UnknownError,
    InvalidDuration
}