using Microsoft.EntityFrameworkCore;

namespace API.Models;


[PrimaryKey(nameof(TripID))]
public class Trip
{
    public required string RouteID { get; set; }
    public required string TripID { get; set; }
    public required string ServiceID { get; set; }
    public required string TripHeadsign { get; set; }
    public int DirectionID { get; set; }
    public required string ShapeID { get; set; }
}