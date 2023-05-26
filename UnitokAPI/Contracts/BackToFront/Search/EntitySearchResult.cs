using Contracts.BackToFront.Full.Entities;

namespace Contracts.BackToFront.Search;

public class EntitySearchResult
{
    public EntitySearchResult(IEnumerable<UserFull?> users, IEnumerable<TokenFull> tokens)
    {
        Users = users;
        Tokens = tokens;
    }

    public IEnumerable<UserFull> Users { get; set; }
    public IEnumerable<TokenFull> Tokens { get; set; }
}