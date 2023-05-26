using System.ComponentModel.DataAnnotations.Schema;
using Domain.ActivityLogs;
using Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey("Id")]
public class Token
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? FileId { get; set; }
    public string Name { get; set; }
    public TokenType Type { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public TokenCreationLog TokenCreationLog { get; set; }
    public List<AuctionFinishLog> TokenAuctions { get; set; }
    [InverseProperty("CreatedTokens")] public User Author { get; set; }
    [InverseProperty("OwnedTokens")] public User Owner { get; set; }

    public Token()
    {
    }

    public Token(string name, TokenType type, string description, User author)
    {
        Name = name;
        Type = type;
        Description = description;
        CreatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        Author = author;
        Owner = author;
    }
}