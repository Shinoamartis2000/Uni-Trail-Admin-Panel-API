// Interfaces/INotificationService.cs
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Interfaces;

public interface INotificationService
{
    Task<List<Notification>> GetAllNotifications();
    Task<bool> SendNotification(NotificationRequest request);
    Task<bool> ScheduleNotification(ScheduledNotificationRequest request);
}