// Controllers/DashboardController.cs
using Microsoft.AspNetCore.Mvc;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController(IAdminService adminService) : ControllerBase
{
    [HttpGet("statistics")]
    public async Task<ActionResult<DashboardStatistics>> GetDashboardStatistics()
    {
        return await adminService.GetDashboardStatistics();
    }

    [HttpGet("recent-activity")]
    public async Task<ActionResult<List<ActivityLog>>> GetRecentActivity([FromQuery] int count = 5)
    {
        return await adminService.GetRecentActivities(count);
    }
}



// Similar controllers for Events, Users, etc.