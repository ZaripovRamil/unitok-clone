using Domain.Entities;

namespace Domain.Accessors.Entities;

public interface IDbBidAccessor : IDbAccessor
{
    public Task Add(Bid bid);
    public Task<Bid?> GetById(string id);
    public Task Delete(Bid bid);
    IEnumerable<Bid> GetAll();
}