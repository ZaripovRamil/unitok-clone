using Contracts.BackToFront.Full.Entities;
using Contracts.BackToFront.Search;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using Services.Abstraction.Search;

namespace Services.Search;

public class ShittyEngine : ISearchEngine
{
    private readonly ITokenReader _tokenReader;
    private readonly IUserReader _userReader;


    public ShittyEngine(IUserReader userReader, ITokenReader tokenReader)
    {
        _userReader = userReader;
        _tokenReader = tokenReader;
    }

    public IEnumerable<TokenFull> SearchTokens(string query)
    {
        return _tokenReader.Read().Where(t => QueryContained(t, query));
    }

    public IEnumerable<UserFull> SearchUsers(string query)
    {
        return _userReader.Read().Where(u => QueryContained(u, query));
    }

    public EntitySearchResult Search(string query)
    {
        return new EntitySearchResult(_userReader.Read().Where(u => QueryContained(u, query)).Take(10),
            _tokenReader.Read().Where(t => QueryContained(t, query)).Take(10));
    }

    private bool QueryContained(TokenFull token, string query)
        => token.Name.Contains(query);

    private static bool QueryContained(UserFull user, string query)
        => user.UserName.Contains(query) || user.FullName.Contains(query);
}