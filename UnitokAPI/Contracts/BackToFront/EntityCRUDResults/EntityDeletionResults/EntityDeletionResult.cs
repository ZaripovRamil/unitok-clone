using Contracts.InterService.EntityValidationResults.Deletion.Codes;

namespace Contracts.BackToFront.EntityCRUDResults.EntityDeletionResults;

public class EntityDeletionResult
{
    private static readonly Dictionary<EntityDeletionValidationCode, string> Messages = new()
    {
        { EntityDeletionValidationCode.Success, "Success" },
        { EntityDeletionValidationCode.NoSuchItem, "No such item" }
    };

    public EntityDeletionResult(EntityDeletionValidationCode code)
    {
        IsSuccessful = code == EntityDeletionValidationCode.Success;
        ResultMessage = Messages[code];
    }

    public bool IsSuccessful { get; set; }
    public string ResultMessage { get; set; }
}