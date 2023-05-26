using Domain.Accessors.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Accessors.Entities;

public class DbAuctionAccessor : DbAccessor, IDbAuctionAccessor
{
    public async Task Add(Auction auction)
    {
        await DbContext.Auctions.AddAsync(auction);
        await DbContext.SaveChangesAsync();
    }

    public async Task<Auction?> GetById(string id) =>
        await DbContext.Auctions.Include(a => a.Bids)
            .ThenInclude(b => b.Bidder)
            .Include(a => a.Token)
            .ThenInclude(t => t.Author)
            .Include(a => a.Token)
            .ThenInclude(t => t.Owner)
            .Include(a => a.Winner)
            .FirstOrDefaultAsync(a => a.Id == id);

    public DbAuctionAccessor(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task Delete(Auction auction)
    {
        DbContext.Auctions.Remove(auction);
        await DbContext.SaveChangesAsync();
    }

    public IEnumerable<Auction> GetAll()
    {
        return DbContext.Auctions
            .Include(a => a.Bids)
            .ThenInclude(b => b.Bidder)
            .Include(a => a.Token)
            .ThenInclude(t => t.Author)
            .Include(a => a.Token)
            .ThenInclude(t => t.Owner)
            .Include(a => a.Winner);
    }
}