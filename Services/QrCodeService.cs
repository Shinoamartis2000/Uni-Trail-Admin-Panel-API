using Microsoft.EntityFrameworkCore;
using QRCoder;
using UniTrail.Admin.Data;
using UniTrail.Admin.Interfaces;
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Services;

public class QrCodeService(AppDbContext context) : IQrCodeService
{
    public async Task<byte[]?> GenerateQrCode(int poiId)
    {
        var poi = await context.PointsOfInterest.FindAsync(poiId);
        if (poi == null) return null;

        var qrContent = $"unitrail://poi/{poiId}";
        using var qrGenerator = new QRCodeGenerator();
        using var qrData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrData);
        return qrCode.GetGraphic(20);
    }

    public async Task<List<QrCodeInfo>> GetAllQrCodes()
    {
        return await context.PointsOfInterest
            .Where(p => p.IsActive)
            .Select(p => new QrCodeInfo
            {
                PoiId = p.Id,
                PoiName = p.Name,
                GeneratedDate = DateTime.Now
            })
            .ToListAsync();
    }

    public async Task<List<int>> BatchGenerateQrCodes(List<int> poiIds)
    {
        var validPoiIds = await context.PointsOfInterest
            .Where(p => poiIds.Contains(p.Id))
            .Select(p => p.Id)
            .ToListAsync();

        return validPoiIds;
    }
}