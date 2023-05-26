using Domain.Entities;

namespace Domain.Accessors.Entities;

public interface IDbAuctionAccessor : IDbAccessor
{
    public Task Add(Auction auction);
    public Task<Auction?> GetById(string id);
    public Task Delete(Auction auction);
    IEnumerable<Auction> GetAll();
}