
using System.Diagnostics;
using API.Data;
using API.Services;
using GeoCoordinatePortable;

namespace API.Models;

public class Router
{
    private readonly ILocationService _locationService;
    private readonly GTFSDbContext _context;

    private (GeoCoordinate, GeoCoordinate) startAndEnd;
    private readonly string scheduleServiceId;
    private readonly DateTime dateTime;

    public Router(ILocationService locationService, GTFSDbContext context, double startLat, double startLon, double endLat, double endLon, string date)
    {
        _locationService = locationService;
        _context = context;
        GeoCoordinate start = new GeoCoordinate(startLat, startLon);
        GeoCoordinate end = new GeoCoordinate(endLat, endLon);
        startAndEnd = (start, end);
        dateTime = DateTime.Parse(date);
        scheduleServiceId = CalendarGTFS.getServiceID(dateTime.DayOfWeek, _context).ServiceID ?? "Weekday";
    }

    public void addLeg(Leg leg)
    {
        _legs.Add(leg);
    }


    public async Task<(List<string>, List<string>)> FindClosestStops()
    {
        var locations = await _locationService.GetLocationsAsync();

        GeoCoordinate myLocation = startAndEnd.Item1;
        GeoCoordinate endLocation = startAndEnd.Item2;

        List<string> startStopIDs = locations
                        .Where(l => l.Coordinate.GetDistanceTo(myLocation) <= 600)
                        .GroupBy(l => l.Stop.StopID.Substring(0, 3))
                        .Select(g => g.First().Stop.StopID).ToList();
        List<string> endStopIDs = locations
                        .Where(l => l.Coordinate.GetDistanceTo(endLocation) <= 600)
                        .GroupBy(l => l.Stop.StopID.Substring(0, 3))
                        .Select(g => g.First().Stop.StopID).ToList();

        return (startStopIDs, endStopIDs);
    }

    public void ConnectStops(List<string> startStops, List<string> endStops) {

        List<string> startRouteIDs = startStops.Select((stopID) => getRouteID(stopID)).ToList();
        List<string> endRouteIDs = endStops.Select((stopID) => getRouteID(stopID)).ToList();
        List<Trip> tripsFromStart = _context.Trips
                                            .Where(trip => startRouteIDs.Contains(trip.RouteID) && scheduleServiceId == trip.ServiceID)
                                            .Select(trip => trip)
                                            .ToList();
        List<Trip> tripsFromEnd = _context.Trips
                                            .Where(trip => endRouteIDs.Contains(trip.RouteID) && scheduleServiceId == trip.ServiceID)
                                            .Select(trip => trip)
                                            .ToList();

        List<string> tripIDsFromStart = tripsFromStart.Select(t => t.TripID).ToList();
        List<string> tripIDsFromEnd = tripsFromEnd.Select(t => t.TripID).ToList();

        List<StopTime> startStopTimes = _context.StopTimes
                                            .Where(stopTime => tripIDsFromStart.Contains(stopTime.TripID) && TimeOnly.FromTimeSpan(dateTime.TimeOfDay) < StopTime.ToTime(stopTime.ArrivalTime))
                                            .Select(stopTime => stopTime)
                                            .ToList();

        /*
         && new TimeOnly(int.Parse(stopTime.ArrivalTime.Split(":")[0]), int.Parse(stopTime.ArrivalTime.Split(":")[1]), int.Parse(stopTime.ArrivalTime.Split(":")[2]) == )
        */                                            
        List<StopTime> endStopTimes = _context.StopTimes
                                            .Where(stopTime => tripIDsFromEnd.Contains(stopTime.TripID))
                                            .Select(stopTime => stopTime)
                                            .ToList();

        IEnumerable<string> ids = tripsFromStart.Join(tripsFromEnd, 
                                                        fromStart => fromStart.TripID,
                                                        fromEnd => fromEnd.TripID,
                                                        (fromStart, fromEnd) => fromStart.TripID);
        
        foreach (var i in startStops) { Console.WriteLine("startStop: " + i); }
        foreach (var i in endStops) { Console.WriteLine("endStop: " + i); }

        foreach (var i in startRouteIDs) { Console.WriteLine("startRouteID: " + i); }
        // foreach (var i in tripIDsFromStart) { Console.WriteLine("tripIDFromStart: " + i); }
        foreach (var i in endRouteIDs) { Console.WriteLine("endRouteID: " + i); }
        // foreach (var i in tripIDsFromEnd) { Console.WriteLine("tripIDFromEnd: " + i); }
        foreach (var i in ids) { Console.WriteLine("SharedTripID: " + i); }
        foreach (var stop in startStopTimes) { Console.WriteLine("startStopTimes: " + stop.TripID + ", " + stop.StopID); }
    }



    Func<string, string> getRouteID = (stopID) => {
                string returnString = stopID[0].ToString(); // First letter of stopID can sometimes be a number: 4, 5, 6 trains, etc,..
                int i = 1;
                while (!char.IsAsciiDigit(stopID[i]))
                {
                    returnString += stopID[i];
                    i++;
                }
                return returnString;
    };
}
/******
*
*

DestinationRoute
(GEOLOCATION, GEOLOCATION) data
@params List Legs
******/