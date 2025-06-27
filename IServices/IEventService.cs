// Interfaces/IEventService.cs
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Interfaces;

public interface IEventService
{
    Task<List<Event>> GetAllEvents(bool upcomingOnly = false);
    Task<Event?> GetEventById(int id);
    Task<Event> CreateEvent(Event eventItem);
    Task<Event?> UpdateEvent(int id, Event eventItem);
    Task<bool> DeleteEvent(int id);
}



