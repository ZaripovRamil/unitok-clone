using Domain.Accessors.EntityOperations;
using Domain.ActivityLogs;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Accessors.EntityOperations;

public class DbAuctionFinishLogAccessor:DbAccessor, IDbAuctionFinishLogAccessor
{
    public DbAuctionFinishLogAccessor(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task Add(AuctionFinishLog auctionFinishLog)
    {
        await DbContext.AuctionFinishLogs.AddAsync(auctionFinishLog);
        await DbContext.SaveChangesAsync();
    }

    public IEnumerable<AuctionFinishLog> GetAll()
    {
        return DbContext.AuctionFinishLogs;
    }
    
    public IEnumerable<AuctionFinishLog> GetByUsername(string username)
    {
        return DbContext.AuctionFinishLogs.Where(a => a.Winner.UserName == username);
    }
}