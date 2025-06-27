// Services/PoiService.cs
using Microsoft.EntityFrameworkCore;
using UniTrail.Admin.Data;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Services;

public class PoiService(AppDbContext context) : IPoiService
{
    public async Task<List<PointOfInterest>> GetAllPois()
    {
        return await context.PointsOfInterest.ToListAsync();
    }

    public async Task<PointOfInterest?> GetPoiById(int id)
    {
        return await context.PointsOfInterest.FindAsync(id);
    }

    public async Task<PointOfInterest> CreatePoi(PointOfInterest poi)
    {
        context.PointsOfInterest.Add(poi);
        await context.SaveChangesAsync();
        return poi;
    }

    public async Task<PointOfInterest?> UpdatePoi(int id, PointOfInterest poi)
    {
        var existingPoi = await context.PointsOfInterest.FindAsync(id);
        if (existingPoi == null) return null;

        existingPoi.Name = poi.Name;
        existingPoi.Description = poi.Description;
        existingPoi.BuildingLocation = poi.BuildingLocation;
        existingPoi.IsActive = poi.IsActive;

        await context.SaveChangesAsync();
        return existingPoi;
    }

    public async Task<bool> DeletePoi(int id)
    {
        var poi = await context.PointsOfInterest.FindAsync(id);
        if (poi == null) return false;

        context.PointsOfInterest.Remove(poi);
        await context.SaveChangesAsync();
        return true;
    }
}