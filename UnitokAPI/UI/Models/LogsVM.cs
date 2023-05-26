using Contracts.BackToFront.LogEntries;

namespace UI.Models;

public class LogsVM
{
    public IEnumerable<AuctionFinishLogDto> AuctionFinishLogs { get; set; }
    public IEnumerable<AuctionParticipanceLogDto> AuctionParticipationLogs { get; set; }
    public IEnumerable<TokenCreationLogDto> TokenCreationLogs { get; set; }
}