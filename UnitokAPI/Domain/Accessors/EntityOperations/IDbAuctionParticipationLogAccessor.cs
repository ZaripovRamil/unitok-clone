using Domain.ActivityLogs;

namespace Domain.Accessors.EntityOperations;

public interface IDbAuctionParticipationLogAccessor
{
    public Task Add(AuctionParticipationLog auctionParticipationLog);
    IEnumerable<AuctionParticipationLog> GetAll();
    IEnumerable<AuctionParticipationLog> GetByUsername(string username);
}