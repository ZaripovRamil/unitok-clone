using Contracts.BackToFront.LogEntries;
using Domain.Accessors.EntityOperations;
using Services.Abstraction;
using Services.Abstraction.LogReaders;

namespace Services.LogReaders;

public class AuctionFinishLogReader:IAuctionFinishLogReader
{
    private readonly IDbAuctionFinishLogAccessor _logAccessor;
    private readonly IDtoCreator _dtoCreator;

    public AuctionFinishLogReader(IDbAuctionFinishLogAccessor logAccessor, IDtoCreator dtoCreator)
    {
        _logAccessor = logAccessor;
        _dtoCreator = dtoCreator;
    }

    public IEnumerable<AuctionFinishLogDto> Read()
    {
        return _logAccessor.GetAll().Select(l=>_dtoCreator.Create(l));
    }
    
    public IEnumerable<AuctionFinishLogDto> GetByUsername(string username)
    {
        return _logAccessor.GetByUsername(username).Select(l=>_dtoCreator.Create(l));
    }
}