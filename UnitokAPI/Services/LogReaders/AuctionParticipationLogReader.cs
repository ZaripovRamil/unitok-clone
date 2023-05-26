using Contracts.BackToFront.LogEntries;
using Domain.Accessors.EntityOperations;
using Services.Abstraction;
using Services.Abstraction.LogReaders;

namespace Services.LogReaders;

public class AuctionParticipationLogReader : IAuctionParticipationLogReader
{
    private readonly IDbAuctionParticipationLogAccessor _logAccessor;
    private readonly IDtoCreator _dtoCreator;

    public AuctionParticipationLogReader(IDbAuctionParticipationLogAccessor logAccessor, IDtoCreator dtoCreator)
    {
        _logAccessor = logAccessor;
        _dtoCreator = dtoCreator;
    }

    public IEnumerable<AuctionParticipanceLogDto> Read()
    {
        return _logAccessor.GetAll().Select(l => _dtoCreator.Create(l));
    }

    public IEnumerable<AuctionParticipanceLogDto> GetByUsername(string username)
    {
        return _logAccessor.GetByUsername(username).Select(l => _dtoCreator.Create(l));
    }
}