namespace Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;

public class EntityUpdateResult
{
    public bool IsSuccessful { get; set; }
    public string ResultMessage { get; set; } = "DefaultMessage";
}