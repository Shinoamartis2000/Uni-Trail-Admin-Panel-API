// Services/IPoiService.cs
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Interfaces;

public interface IPoiService
{
    Task<List<PointOfInterest>> GetAllPois();
    Task<PointOfInterest?> GetPoiById(int id);
    Task<PointOfInterest> CreatePoi(PointOfInterest poi);
    Task<PointOfInterest?> UpdatePoi(int id, PointOfInterest poi);
    Task<bool> DeletePoi(int id);
}