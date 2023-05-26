using Domain.Accessors.EntityOperations;
using Domain.ActivityLogs;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Accessors.EntityOperations;

public class DbTokenCreationLogAccessor:DbAccessor, IDbTokenCreationLogAccessor
{
    public DbTokenCreationLogAccessor(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task Add(TokenCreationLog tokenCreationLog)
    {
        await DbContext.TokenCreationLogs.AddAsync(tokenCreationLog);
        await DbContext.SaveChangesAsync();
    }

    public IEnumerable<TokenCreationLog> GetAll()
    {
        return DbContext.TokenCreationLogs;
    }
    public IEnumerable<TokenCreationLog> GetByUsername(string username)
    {
        return DbContext.TokenCreationLogs.Where(a => a.Author.UserName == username);
    }
}