using Domain.ActivityLogs.Enums;
using Domain.Entities;

namespace Domain.ActivityLogs;

public class TokenCreationLog : ActivityLog
{
    public TokenCreationLog(Token token, User author, DateTime creationTime) : this()
    {
        Token = token;
        Author = author;
        CreationTime = creationTime;
    }

    public TokenCreationLog()
    {
        Code = ActivityCode.TokenCreation;
    }

    public string TokenId { get; set; }
    public Token Token { get; set; }
    public User Author { get; set; }
    public DateTime CreationTime { get; set; }
}