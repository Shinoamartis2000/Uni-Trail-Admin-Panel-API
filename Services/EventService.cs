using Microsoft.EntityFrameworkCore;
using UniTrail.Admin.Data;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Services;

public class EventService(AppDbContext context) : IEventService
{
    public async Task<List<Event>> GetAllEvents(bool upcomingOnly = false)
    {
        var query = context.Events.AsQueryable();

        if (upcomingOnly)
        {
            query = query.Where(e => e.StartTime > DateTime.Now);
        }

        return await query.ToListAsync();
    }

    public async Task<Event?> GetEventById(int id)
    {
        return await context.Events.FindAsync(id);
    }

    public async Task<Event> CreateEvent(Event eventItem)
    {
        context.Events.Add(eventItem);
        await context.SaveChangesAsync();
        return eventItem;
    }

    public async Task<Event?> UpdateEvent(int id, Event eventItem)
    {
        var existingEvent = await context.Events.FindAsync(id);
        if (existingEvent == null) return null;

        existingEvent.Title = eventItem.Title;
        existingEvent.Description = eventItem.Description;
        existingEvent.StartTime = eventItem.StartTime;
        existingEvent.EndTime = eventItem.EndTime;
        existingEvent.Location = eventItem.Location;
        existingEvent.Organizer = eventItem.Organizer;

        await context.SaveChangesAsync();
        return existingEvent;
    }

    public async Task<bool> DeleteEvent(int id)
    {
        var eventItem = await context.Events.FindAsync(id);
        if (eventItem == null) return false;

        context.Events.Remove(eventItem);
        await context.SaveChangesAsync();
        return true;
    }
}