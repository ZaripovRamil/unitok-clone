using Domain.ActivityLogs.Enums;

namespace Contracts.BackToFront.LogEntries;

public class ActivityLogDto
{
    public string Id { get; set; }
    public ActivityCode Code { get; set; }
}