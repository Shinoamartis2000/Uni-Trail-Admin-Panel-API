// Services/AdminService.cs
using Microsoft.EntityFrameworkCore;
using UniTrail.Admin.Data;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Services;

public class AdminService(AppDbContext context) : IAdminService
{
    public async Task<DashboardStatistics> GetDashboardStatistics()
    {
        var today = DateTime.Today;

        return new DashboardStatistics
        {
            ActiveUsers = await context.Users.CountAsync(u => u.IsActive),
            PointsOfInterest = await context.PointsOfInterest.CountAsync(),
            UpcomingEvents = await context.Events.CountAsync(e => e.StartTime > DateTime.Now),
            DailyNavigations = await context.ActivityLogs
                .CountAsync(a => a.Timestamp.Date == today &&
                               a.ActivityType == "Navigation")
        };
    }

    public async Task<List<ActivityLog>> GetRecentActivities(int count = 5)
    {
        return await context.ActivityLogs
            .OrderByDescending(a => a.Timestamp)
            .Take(count)
            .ToListAsync();
    }
}