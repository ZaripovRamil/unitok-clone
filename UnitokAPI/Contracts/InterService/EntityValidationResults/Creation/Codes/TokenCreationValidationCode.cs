namespace Contracts.InterService.EntityValidationResults.Creation.Codes;

public enum TokenCreationValidationCode
{
    Successful = EntityCreationValidationCode.Successful,
    InvalidAuthor,
    UnknownError,
    EmptyName
}