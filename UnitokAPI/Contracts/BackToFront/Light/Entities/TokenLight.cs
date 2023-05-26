namespace Contracts.BackToFront.Light.Entities;

public class TokenLight
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Type { get; set; }
    public string FileId { get; set; }
    public UserLight Author { get; set; }
    public UserLight Owner { get; set; }
}