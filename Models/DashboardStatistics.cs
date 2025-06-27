// Models/DashboardStatistics.cs
namespace UniTrail.Admin.Models;

public class DashboardStatistics
{
    public int ActiveUsers { get; set; }
    public int PointsOfInterest { get; set; }
    public int UpcomingEvents { get; set; }
    public int DailyNavigations { get; set; }
}