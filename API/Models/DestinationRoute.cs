
using GeoCoordinatePortable;

namespace API.Models;

public class DestinationRoute
{
    private List<Leg> _legs = new List<Leg>();
    public void addLeg(Leg leg)
    {
        _legs.Add(leg);
    }

    public required string ServiceId { get; set; }

    public static List<string> FindClosestStops(List<Location> locations, double lat, double lon)
    {
        GeoCoordinate myLocation = new GeoCoordinate(lat, lon);
        // List<string> stopIDs = locations
        //                         .Where(l => l.Coordinate.GetDistanceTo(myLocation) <= 600)
        //                         .Select(l => l.Stop.StopID)
        //                         .ToList();
        List<string> stopIDs = locations
                                .Where(l => l.Coordinate.GetDistanceTo(myLocation) <= 600)
                                .GroupBy(l => l.Stop.StopID.Substring(0, 3))
                                .Select(g => g.First().Stop.StopID)
                                .ToList();
                                // .GroupBy(l => l.Stop.StopID)
                                // .Select(grouped => grouped.First().Stop.StopID)
        return stopIDs;
    }

}


/****

    stopIDs =
        (from l in locations
        where l.Item2.GetDistanceTo(myLocation) <= 500
        group l by l.Item1.StopID into grouped
        select grouped.First().Item1.StopID).ToList();
    

*/ 