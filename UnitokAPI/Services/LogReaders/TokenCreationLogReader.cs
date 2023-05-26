using Contracts.BackToFront.LogEntries;
using Domain.Accessors.EntityOperations;
using Services.Abstraction;
using Services.Abstraction.LogReaders;

namespace Services.LogReaders;

public class TokenCreationLogReader:ITokenCreationLogReader
{
    private readonly IDbTokenCreationLogAccessor _logAccessor;
    private readonly IDtoCreator _dtoCreator;

    public TokenCreationLogReader(IDbTokenCreationLogAccessor logAccessor, IDtoCreator dtoCreator)
    {
        _logAccessor = logAccessor;
        _dtoCreator = dtoCreator;
    }

    public IEnumerable<TokenCreationLogDto> Read()
    {
        return _logAccessor.GetAll().Select(l=>_dtoCreator.Create(l));
    }
    public IEnumerable<TokenCreationLogDto> GetByUsername(string username)
    {
        return _logAccessor.GetByUsername(username).Select(l=>_dtoCreator.Create(l));
    }
}