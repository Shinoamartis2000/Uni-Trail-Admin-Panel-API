// Models/NotificationRequest.cs
namespace UniTrail.Admin.Models;

public class NotificationRequest
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string TargetAudience { get; set; } = "all";
}