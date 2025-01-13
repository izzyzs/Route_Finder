using GeoCoordinatePortable;

namespace API.Models;

public class Location
{
    private (Stop, GeoCoordinate) data;
    public Location(Stop item1, GeoCoordinate item2)
    {
        data = (item1, item2);
    }

    public Stop Stop
    {
        get => data.Item1;
        set => data.Item1 = value;
    }
    public GeoCoordinate Coordinate
    {
        get => data.Item2;
        set => data.Item2 = value;
    }

    public override string ToString()
    {
        return $"({data.Item1}, {data.Item2})";
    }
}