using Contracts.InterService.EntityValidationResults.Creation.Codes;

namespace Contracts.InterService.EntityValidationResults.Creation;

public class EntityCreationValidationResult
{
    public EntityCreationValidationResult(int validationCode)
    {
        ValidationCode = validationCode;
        IsValid = ValidationCode == (int) EntityCreationValidationCode.Successful;
    }

    public bool IsValid { get; set; }
    public int ValidationCode { get; set; }
}