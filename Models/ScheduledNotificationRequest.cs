
// Models/ScheduledNotificationRequest.cs
namespace UniTrail.Admin.Models;

public class ScheduledNotificationRequest : NotificationRequest
{
    public DateTime SendTime { get; set; }
}