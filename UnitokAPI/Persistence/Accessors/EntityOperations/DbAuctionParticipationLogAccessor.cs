using Domain.Accessors.EntityOperations;
using Domain.ActivityLogs;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Accessors.EntityOperations;

public class DbAuctionParticipationLogAccessor:DbAccessor, IDbAuctionParticipationLogAccessor
{
    public DbAuctionParticipationLogAccessor(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task Add(AuctionParticipationLog auctionParticipationLog)
    {
        await DbContext.AuctionParticipationLogs.AddAsync(auctionParticipationLog);
        await DbContext.SaveChangesAsync();
    }

    public IEnumerable<AuctionParticipationLog> GetAll()
    {
        return DbContext.AuctionParticipationLogs;
    }
    public IEnumerable<AuctionParticipationLog> GetByUsername(string username)
    {
        return DbContext.AuctionParticipationLogs.Where(a => a.Bidder.UserName == username);
    }
}