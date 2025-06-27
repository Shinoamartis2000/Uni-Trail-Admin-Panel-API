using Microsoft.EntityFrameworkCore;
using UniTrail.Admin.Data;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Services;

public class BuildingService(AppDbContext context) : IBuildingService
{
    public async Task<List<Building>> GetAllBuildings()
    {
        return await context.Buildings
            .Include(b => b.PointsOfInterest)
            .ToListAsync();
    }

    public async Task<Building?> GetBuildingById(int id)
    {
        return await context.Buildings
            .Include(b => b.PointsOfInterest)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Building> CreateBuilding(Building building)
    {
        context.Buildings.Add(building);
        await context.SaveChangesAsync();
        return building;
    }

    public async Task<Building?> UpdateBuilding(int id, Building building)
    {
        var existingBuilding = await context.Buildings.FindAsync(id);
        if (existingBuilding == null) return null;

        existingBuilding.Name = building.Name;
        existingBuilding.Code = building.Code;
        existingBuilding.Description = building.Description;
        existingBuilding.Latitude = building.Latitude;
        existingBuilding.Longitude = building.Longitude;
        existingBuilding.Address = building.Address;
        existingBuilding.ImageUrl = building.ImageUrl;
        existingBuilding.IsActive = building.IsActive;
        existingBuilding.LastUpdated = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return existingBuilding;
    }

    public async Task<bool> DeleteBuilding(int id)
    {
        var building = await context.Buildings.FindAsync(id);
        if (building == null) return false;

        // Check if building has associated POIs
        if (await context.PointsOfInterest.AnyAsync(p => p.BuildingId == id))
        {
            throw new InvalidOperationException("Cannot delete building with associated points of interest");
        }

        context.Buildings.Remove(building);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<PointOfInterest>> GetBuildingPois(int buildingId)
    {
        return await context.PointsOfInterest
            .Where(p => p.BuildingId == buildingId)
            .ToListAsync();
    }

    public async Task<int> GetBuildingOccupancy(int buildingId)
    {
        // This could use real-time data from navigation system in the future
        return await context.ActivityLogs
            .CountAsync(a => a.Location.Contains("Building " + buildingId) &&
                           a.Timestamp > DateTime.Now.AddHours(-1));
    }
}