using Contracts.InterService.EntityValidationResults.Deletion.Codes;
using Contracts.InterService.EntityValidationResults.Update.Codes;

namespace Contracts.InterService.EntityValidationResults.Update;

public class EntityUpdateValidationResult
{

    public EntityUpdateValidationResult(int validationCode)
    {
        ValidationCode = validationCode;
        IsValid = ValidationCode == (int) EntityDeletionValidationCode.Success;
    }

    public bool IsValid { get; set; }
    public int ValidationCode { get; set; }
    
}