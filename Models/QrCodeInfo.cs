
// Models/QrCodeInfo.cs
namespace UniTrail.Admin.Models;

public class QrCodeInfo
{
    public int PoiId { get; set; }
    public string PoiName { get; set; } = string.Empty;
    public DateTime GeneratedDate { get; set; }
}

