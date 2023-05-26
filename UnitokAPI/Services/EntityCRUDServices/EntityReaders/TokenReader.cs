using Contracts.BackToFront.Full.Entities;
using Domain.Accessors.Entities;
using Domain.Entities;
using Services.Abstraction;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using Services.Extensions;

namespace Services.EntityCRUDServices.EntityReaders;

public class TokenReader : ITokenReader
{
    private readonly IDbTokenAccessor _tokenAccessor;
    private readonly IDtoCreator _dtoCreator;

    public TokenReader(IDbTokenAccessor tokenAccessor, IDtoCreator dtoCreator)
    {
        _tokenAccessor = tokenAccessor;
        _dtoCreator = dtoCreator;
    }

    public IEnumerable<TokenFull?> Read()
    {
        return _tokenAccessor.GetAll().Select(t => _dtoCreator.CreateFull(t));
    }

    public async Task<TokenFull?> Read(string id)
    {
        return _dtoCreator.CreateFull(await _tokenAccessor.GetById(id));
    }

    public IEnumerable<TokenFull?> Read(Predicate<Token> filter, Func<Token, IComparable> orderBy, int pageNumber,
        int pageSize, bool orderByAscending = true)
    {
        return _tokenAccessor.GetAll()
            .Where(t => filter(t))
            .FlexibleOrderBy(orderBy, orderByAscending)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .Select(t => _dtoCreator.CreateFull(t));
    }
}