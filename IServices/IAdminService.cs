// Services/IAdminService.cs
using UniTrail.Admin.Data;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Interfaces;

public interface IAdminService
{
    Task<DashboardStatistics> GetDashboardStatistics();
    Task<List<ActivityLog>> GetRecentActivities(int count = 5);
}







// Similar implementations for IEventService and EventService...