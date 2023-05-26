using Contracts.BackToFront.Full.Entities;
using Domain.Entities;

namespace Services.Abstraction.EntityCRUDServices.EntityReaders;

public interface ITokenReader
{
    public IEnumerable<TokenFull?> Read();
    public Task<TokenFull?> Read(string id);

    public IEnumerable<TokenFull?> Read(Predicate<Token> filter, Func<Token, IComparable> orderBy, int pageNumber,
        int pageSize, bool orderByAscending);
}