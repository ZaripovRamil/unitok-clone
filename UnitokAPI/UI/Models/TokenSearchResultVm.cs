using Contracts.BackToFront.Full.Entities;

namespace UI.Models;

public class TokenSearchResultVm
{
    public TokenSearchResultVm(TokenFull token)
    {
        Name = token.Name;
        PreviewId = token.PreviewId;
        Type = token.Type.ToString();
    }

    public string PreviewId { get; set; }

    public string Name { get; set; }
    public string Type { get; set; }
}