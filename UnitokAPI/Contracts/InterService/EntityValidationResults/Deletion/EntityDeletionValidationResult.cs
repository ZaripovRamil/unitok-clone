using Contracts.InterService.EntityValidationResults.Deletion.Codes;

namespace Contracts.InterService.EntityValidationResults.Deletion;

public class EntityDeletionValidationResult
{
    public EntityDeletionValidationResult(EntityDeletionValidationCode validationCode)
    {
        ValidationCode = validationCode;
        IsValid = ValidationCode == (int) EntityDeletionValidationCode.Success;
    }

    public bool IsValid { get; set; }
    public EntityDeletionValidationCode ValidationCode { get; set; }
}