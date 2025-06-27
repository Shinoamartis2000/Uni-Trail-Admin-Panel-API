// Models/PointOfInterest.cs
namespace UniTrail.Admin.Models;

public class PointOfInterest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int BuildingId { get; set; }  // Foreign key

    // Navigation property (renamed to avoid conflict)
    public Building? BuildingLocation { get; set; }

    public bool IsActive { get; set; }
    public string Floor { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}