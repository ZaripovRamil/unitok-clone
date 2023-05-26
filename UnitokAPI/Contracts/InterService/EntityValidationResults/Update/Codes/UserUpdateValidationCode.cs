namespace Contracts.InterService.EntityValidationResults.Update.Codes;

public enum UserUpdateValidationCode
{
    Successful = EntityUpdateValidationCode.Success,
    PasswordMismatch,
    InvalidSeedPhrase,
    NoSuchItem,
    EmptyValue,
    UnknownError,
    
}