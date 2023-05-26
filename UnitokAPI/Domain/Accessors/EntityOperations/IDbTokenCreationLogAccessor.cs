using Domain.ActivityLogs;

namespace Domain.Accessors.EntityOperations;

public interface IDbTokenCreationLogAccessor
{
    public Task Add(TokenCreationLog tokenCreationLog);
    IEnumerable<TokenCreationLog> GetAll();
    IEnumerable<TokenCreationLog> GetByUsername(string username);
}