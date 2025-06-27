// Controllers/PoiController.cs
using Microsoft.AspNetCore.Mvc;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Controllers;

[ApiController]
[Route("api/poi")]
public class PoiController(IPoiService poiService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PointOfInterest>>> GetAllPois()
    {
        return await poiService.GetAllPois();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PointOfInterest>> GetPoiById(int id)
    {
        var poi = await poiService.GetPoiById(id);
        if (poi == null) return NotFound();
        return poi;
    }

    [HttpPost]
    public async Task<ActionResult<PointOfInterest>> CreatePoi(PointOfInterest poi)
    {
        var createdPoi = await poiService.CreatePoi(poi);
        return CreatedAtAction(nameof(GetPoiById), new { id = createdPoi.Id }, createdPoi);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePoi(int id, PointOfInterest poi)
    {
        if (id != poi.Id) return BadRequest();

        var updatedPoi = await poiService.UpdatePoi(id, poi);
        if (updatedPoi == null) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePoi(int id)
    {
        var result = await poiService.DeletePoi(id);
        if (!result) return NotFound();
        return NoContent();
    }
}