using Contracts.BackToFront.Light;
using Contracts.BackToFront.Light.Entities;

namespace Contracts.BackToFront.LogEntries;

public class TokenCreationLogDto:ActivityLogDto
{
    public TokenLight Token { get; set; }
    public UserLight Author { get; set; }
    public DateTime CreationTime { get; set; }
}