namespace API.Json;

public class FindRouteJson
{
    public double StartLat { get; set; }
    public double StartLon { get; set; }
    public double EndLat { get; set; }
    public double EndLon { get; set; }
    public required string TravelDateTime { get; set; }
}