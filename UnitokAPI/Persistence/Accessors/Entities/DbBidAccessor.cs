using Domain.Accessors.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Accessors.Entities;

public class DbBidAccessor : DbAccessor, IDbBidAccessor
{
    public async Task Add(Bid bid)
    {
        await DbContext.Bids.AddAsync(bid);
        await DbContext.SaveChangesAsync();
    }

    public async Task<Bid?> GetById(string id) =>
        await DbContext.Bids.Include(b => b.Bidder)
            .Include(b => b.Auction)
            .ThenInclude(a => a.Token)
            .ThenInclude(t => t.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

    public DbBidAccessor(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task Delete(Bid bid)
    {
        DbContext.Bids.Remove(bid);
        await DbContext.SaveChangesAsync();
    }

    public IEnumerable<Bid> GetAll()
    {
        return DbContext.Bids
            .Include(b => b.Auction)
            .ThenInclude(a => a.Token)
            .ThenInclude(t => t.Author);
    }
}