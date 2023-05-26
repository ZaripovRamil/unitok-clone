using Contracts.BackToFront.LogEntries;

namespace Services.Abstraction.LogReaders;

public interface IAuctionFinishLogReader
{
    public IEnumerable<AuctionFinishLogDto> Read();
    public IEnumerable<AuctionFinishLogDto> GetByUsername(string username);
}