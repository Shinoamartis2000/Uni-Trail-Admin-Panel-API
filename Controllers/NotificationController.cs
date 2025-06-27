using Microsoft.AspNetCore.Mvc;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Notification>>> GetAllNotifications()
    {
        return await notificationService.GetAllNotifications();
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification(NotificationRequest request)
    {
        var result = await notificationService.SendNotification(request);
        if (!result) return BadRequest("Failed to send notification");
        return Ok();
    }

    [HttpPost("schedule")]
    public async Task<IActionResult> ScheduleNotification(ScheduledNotificationRequest request)
    {
        var result = await notificationService.ScheduleNotification(request);
        if (!result) return BadRequest("Failed to schedule notification");
        return Ok();
    }
}