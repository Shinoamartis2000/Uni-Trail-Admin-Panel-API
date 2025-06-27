// Models/Notification.cs
namespace UniTrail.Admin.Models;

public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string TargetAudience { get; set; } = "all"; // all, students, staff, etc.
    public DateTime? ScheduledTime { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Sent, Failed
}

