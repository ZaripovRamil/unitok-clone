using Domain.Accessors.Entities;

namespace Persistence.Accessors;

public class DbAccessor : IDbAccessor
{
    public AppDbContext DbContext { get; }

    public DbAccessor(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}