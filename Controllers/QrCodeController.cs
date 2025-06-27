using Microsoft.AspNetCore.Mvc;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Controllers;

[ApiController]
[Route("api/qrcodes")]
public class QrCodeController(IQrCodeService qrCodeService) : ControllerBase
{
    [HttpGet("generate/{poiId}")]
    public async Task<IActionResult> GenerateQrCode(int poiId)
    {
        var qrCode = await qrCodeService.GenerateQrCode(poiId);
        if (qrCode == null) return NotFound("POI not found");

        return File(qrCode, "image/png");
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<QrCodeInfo>>> GetAllQrCodes()
    {
        return await qrCodeService.GetAllQrCodes();
    }

    [HttpPost("batch-generate")]
    public async Task<ActionResult<List<int>>> BatchGenerateQrCodes([FromBody] List<int> poiIds)
    {
        return await qrCodeService.BatchGenerateQrCodes(poiIds);
    }
}