using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Controllers;

[ApiController]
[Route("api/buildings")]
public class BuildingController(IBuildingService buildingService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Building>>> GetAllBuildings()
    {
        return await buildingService.GetAllBuildings();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Building>> GetBuildingById(int id)
    {
        var building = await buildingService.GetBuildingById(id);
        if (building == null) return NotFound();
        return building;
    }

    [HttpGet("{id}/pois")]
    public async Task<ActionResult<List<PointOfInterest>>> GetBuildingPois(int id)
    {
        return await buildingService.GetBuildingPois(id);
    }

    [HttpGet("{id}/occupancy")]
    public async Task<ActionResult<int>> GetBuildingOccupancy(int id)
    {
        return await buildingService.GetBuildingOccupancy(id);
    }

    [HttpPost]
    public async Task<ActionResult<Building>> CreateBuilding(Building building)
    {
        var createdBuilding = await buildingService.CreateBuilding(building);
        return CreatedAtAction(nameof(GetBuildingById), new { id = createdBuilding.Id }, createdBuilding);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBuilding(int id, Building building)
    {
        if (id != building.Id) return BadRequest();

        var updatedBuilding = await buildingService.UpdateBuilding(id, building);
        if (updatedBuilding == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuilding(int id)
    {
        try
        {
            var result = await buildingService.DeleteBuilding(id);
            if (!result) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}