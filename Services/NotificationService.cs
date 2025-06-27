using Microsoft.EntityFrameworkCore;
using UniTrail.Admin.Data;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Services;

public class NotificationService(AppDbContext context) : INotificationService
{
    public async Task<List<Notification>> GetAllNotifications()
    {
        return await context.Notifications
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> SendNotification(NotificationRequest request)
    {
        // In a real implementation, integrate with Firebase Cloud Messaging or similar
        var notification = new Notification

        {
            Title = request.Title,
            Message = request.Message,
            TargetAudience = request.TargetAudience,
            CreatedAt = DateTime.Now,
            Status = "Sent"
        };

        context.Notifications.Add(notification);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ScheduleNotification(ScheduledNotificationRequest request)
    {
        var notification = new Notification
        {
            Title = request.Title,
            Message = request.Message,
            TargetAudience = request.TargetAudience,
            ScheduledTime = request.SendTime,
            CreatedAt = DateTime.Now,
            Status = "Scheduled"
        };

        context.Notifications.Add(notification);
        await context.SaveChangesAsync();
        return true;
    }

    // Services/BuildingService.cs
    public async Task<List<Building>> GetAllBuildings()
    {
        return await context.Buildings
            .Include(b => b.PointsOfInterest)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Building?> GetBuildingById(int id)
    {
        return await context.Buildings
            .Include(b => b.PointsOfInterest)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}