using Contracts.BackToFront.LogEntries;

namespace Services.Abstraction.LogReaders;

public interface ITokenCreationLogReader
{
    public IEnumerable<TokenCreationLogDto> Read();
    public IEnumerable<TokenCreationLogDto> GetByUsername(string username);
}