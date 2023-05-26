using Contracts.BackToFront.Search;

namespace UI.Models;

public class EntitySearchResultVm
{
    public List<UserSearchResultVm> Users { get; set; }
    public List<TokenSearchResultVm> Tokens { get; set; }

    public EntitySearchResultVm(EntitySearchResult entitySearchResult)
    {
        Users = entitySearchResult.Users.Select(u => new UserSearchResultVm(u)).ToList();
        Tokens = entitySearchResult.Tokens.Select(t => new TokenSearchResultVm(t)).ToList();
    }
}