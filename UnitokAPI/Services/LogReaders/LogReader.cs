using Services.Abstraction.LogReaders;

namespace Services.LogReaders;

public class LogReader : ILogReader
{
    public LogReader(IAuctionFinishLogReader auctionFinishLogReader, IAuctionParticipationLogReader auctionParticipationLogReader,
        ITokenCreationLogReader tokenCreationLogReader)
    {
        AuctionParticipationLogReader = auctionParticipationLogReader;
        AuctionFinishLogReader = auctionFinishLogReader;
        TokenCreationLogReader = tokenCreationLogReader;
    }
    public IAuctionFinishLogReader AuctionFinishLogReader { get; set; }
    public IAuctionParticipationLogReader AuctionParticipationLogReader { get; set; }
    public ITokenCreationLogReader TokenCreationLogReader { get; set; }
}