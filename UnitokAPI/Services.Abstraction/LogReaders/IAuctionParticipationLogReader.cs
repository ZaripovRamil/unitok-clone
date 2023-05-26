using Contracts.BackToFront.LogEntries;

namespace Services.Abstraction.LogReaders;

public interface IAuctionParticipationLogReader
{
    public IEnumerable<AuctionParticipanceLogDto> Read();
    public IEnumerable<AuctionParticipanceLogDto> GetByUsername(string username);
}