// Models/Building.cs
namespace UniTrail.Admin.Models;

public class Building
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    // Navigation property (updated to match)
    public List<PointOfInterest> PointsOfInterest { get; set; } = new();

    // Other properties remain the same...
    public string Description { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Address { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}