// Interfaces/IQrCodeService.cs
using UniTrail.Admin.Models;

namespace UniTrail.Admin.Interfaces;

public interface IQrCodeService
{
    Task<byte[]?> GenerateQrCode(int poiId);
    Task<List<QrCodeInfo>> GetAllQrCodes();
    Task<List<int>> BatchGenerateQrCodes(List<int> poiIds);
}
