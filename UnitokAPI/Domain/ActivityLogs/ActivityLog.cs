using Domain.ActivityLogs.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.ActivityLogs;


[PrimaryKey("Id")]
public class ActivityLog
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public ActivityCode Code { get; set; }
}