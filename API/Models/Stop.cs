using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey(nameof(StopID))]
public class Stop
{
    public required string StopID { get; set; }
    public required string StopName { get; set; }
    public double StopLat { get; set; }
    public double StopLon { get; set; }
    public int? LocationType { get; set; }
    public string? ParentStation { get; set; }
}