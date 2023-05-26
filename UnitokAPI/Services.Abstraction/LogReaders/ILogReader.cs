namespace Services.Abstraction.LogReaders;

public interface ILogReader
{
    public IAuctionFinishLogReader AuctionFinishLogReader { get; set; }
    public IAuctionParticipationLogReader AuctionParticipationLogReader { get; set; }
    public ITokenCreationLogReader TokenCreationLogReader { get; set; }
}