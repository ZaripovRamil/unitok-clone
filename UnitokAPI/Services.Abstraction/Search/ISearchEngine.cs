using Contracts.BackToFront.Search;
using Contracts.BackToFront.Full.Entities;


namespace Services.Abstraction.Search;

public interface ISearchEngine
{
    public IEnumerable<TokenFull> SearchTokens(string query);
    public IEnumerable<UserFull> SearchUsers(string query);
    public EntitySearchResult Search(string query);
}