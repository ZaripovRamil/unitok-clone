using Contracts.BackToFront.Full.Entities;

namespace UI.Models;

public class TokenSearchResultListVm
{
    public TokenSearchResultListVm(IEnumerable<TokenFull> tokens)
    {
        Tokens = tokens.Select(t => new TokenSearchResultVm(t)).ToList();
    }

    public List<TokenSearchResultVm> Tokens { get; set; }
}