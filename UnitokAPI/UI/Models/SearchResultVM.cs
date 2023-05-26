using Contracts.BackToFront.Full.Entities;

namespace UI.Models;

public class SearchResultVM
{
    public SearchResultVM(IEnumerable<TokenFull> tokens, IEnumerable<UserFull> users)
    {
        Tokens = tokens.Select(t => new TokenSearchResultVm(t)).ToList();
        Users = users.Select(u => new UserSearchResultVm(u)).ToList();
    }

    public List<TokenSearchResultVm> Tokens { get; set; }
    public List<UserSearchResultVm> Users { get; set; }
}