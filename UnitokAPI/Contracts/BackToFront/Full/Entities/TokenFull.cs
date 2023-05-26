using Contracts.BackToFront.Light.Entities;

namespace Contracts.BackToFront.Full.Entities;

public class TokenFull
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public string PreviewId { get; set; }
    public string FileId { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserLight Owner { get; set; }
    public UserLight Author { get; set; }
}