using Domain.ActivityLogs;

namespace Domain.Accessors.EntityOperations;

public interface IDbAuctionFinishLogAccessor
{
    public Task Add(AuctionFinishLog auctionFinishLog);
    IEnumerable<AuctionFinishLog> GetAll();
    IEnumerable<AuctionFinishLog> GetByUsername(string username);
}