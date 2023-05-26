namespace Contracts.InterService.EntityValidationResults.Creation.Codes;

public enum RegistrationCode
{
    Successful,
    LoginTaken,
    EmailTaken,
    WeakPassword,
    UnknownError
}