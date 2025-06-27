using Microsoft.AspNetCore.Mvc;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController(IEventService eventService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetAllEvents([FromQuery] bool upcomingOnly = false)
    {
        return await eventService.GetAllEvents(upcomingOnly);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEventById(int id)
    {
        var eventItem = await eventService.GetEventById(id);
        if (eventItem == null) return NotFound();
        return eventItem;
    }

    [HttpPost]
    public async Task<ActionResult<Event>> CreateEvent(Event eventItem)
    {
        var createdEvent = await eventService.CreateEvent(eventItem);
        return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, Event eventItem)
    {
        if (id != eventItem.Id) return BadRequest();

        var updatedEvent = await eventService.UpdateEvent(id, eventItem);
        if (updatedEvent == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var result = await eventService.DeleteEvent(id);
        if (!result) return NotFound();
        return NoContent();
    }
}