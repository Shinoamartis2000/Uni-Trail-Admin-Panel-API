using UniTrail.Admin.Models;

namespace UniTrail.Admin.Interfaces;

public interface IBuildingService
{
    Task<List<Building>> GetAllBuildings();
    Task<Building?> GetBuildingById(int id);
    Task<Building> CreateBuilding(Building building);
    Task<Building?> UpdateBuilding(int id, Building building);
    Task<bool> DeleteBuilding(int id);
    Task<List<PointOfInterest>> GetBuildingPois(int buildingId);
    Task<int> GetBuildingOccupancy(int buildingId); // For future use
}