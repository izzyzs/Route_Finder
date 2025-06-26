using API.Models;
using API.Data;
using CsvHelper;
using CsvHelper.Configuration;
using GeoCoordinatePortable;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using API.Services;
using Microsoft.VisualBasic;
using API.Dtos.GoogleApi;
using System.Text.Json;

const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.sssz";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GTFSDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddHttpClient<GoogleApiClient>();


// List<(Stop, GeoCoordinate)> closeEnough;
// List<char> letters;
// List<string> stopsClosestToMe;
// List<string> stopsClosestToDestination;

builder.Services.AddControllers();

var app = builder.Build();
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<GTFSDbContext>();
//     var locationService = scope.ServiceProvider.GetRequiredService<ILocationService>();
//     var googleApiClient = scope.ServiceProvider.GetRequiredService<GoogleApiClient>();


//     // 40.81292565939048, -73.92588980616152
//     var homeLatLng = (40.83181721457334, -73.91831147657086);
//     var tbsLatLng = (40.82221343186862, -73.9390352088722);
//     var licensingLatLng = (40.70992069554792, -74.00711090867834);
//     double startLat = homeLatLng.Item1;
//     double startLon = homeLatLng.Item2;

//     double endLat = licensingLatLng.Item1;
//     double endLon = licensingLatLng.Item2;

//     List<string> closestStarts, closestEnds;

//     Router router = new Router(locationService, context, googleApiClient, startLat, startLon, endLat, endLon, DateTime.Now.ToString(DateTimeFormat));
//     (closestStarts, closestEnds) = await router.FindClosestStops();
//     // route.ConnectStops(closestStarts, closestEnds);
//     var result = await router.ConnectRoute();
//     var jsonifiedResult = JsonSerializer.Serialize(result);
//     File.WriteAllText("debug.json", jsonifiedResult);


//     // // List<(Stop, GeoCoordinate)> locations =
//     //     (from r in context.Stops
//     //      select (r, new GeoCoordinate(r.StopLat, r.StopLon))).ToList(); 

//     // LocationService l = new LocationService(context, );

//     // List<CalendarGTFS> calendars = (context.CalendarGTFSs.Select(c => c)).ToList();
//     // foreach (var c in calendars)
//     // {
//     //     Console.WriteLine("c.ServiceID\t" + c.ServiceID +
//     //                       "\nc.Monday\t" + c.Monday +
//     //                       "\nc.Tuesday\t" + c.Tuesday +
//     //                       "\nc.Wednesday\t" + c.Wednesday +
//     //                       "\nc.Thursday\t" + c.Thursday +
//     //                       "\nc.Friday\t" + c.Friday +
//     //                       "\nc.Saturday\t" + c.Saturday +
//     //                       "\nc.Sunday\t" + c.Sunday +
//     //                       "\nc.StartDate\t" + c.StartDate +
//     //                       "\nc.EndDate\t" + c.EndDate + 
//     //                       "\nc.IsWeekday\t" + c.IsWeekday + "\n\n"
//     //                     );
//     // // Console.WriteLine(c.Day(10) == 1);
//     // }


//     // stopsClosestToMe = DestinationRoute.FindClosestStops(locations, myLat, myLon);
//     // stopsClosestToDestination = DestinationRoute.FindClosestStops(locations, destinationLat, destinationLon);

//     // closeEnough =
//     //     (from l in locationsList
//     //     where l.Item2.GetDistanceTo(myLocation) <= 500
//     //     select l).ToList();
//     // stopsClosestToMe =
//     //     (from l in locationsList
//     //     where l.Item2.GetDistanceTo(myLocation) <= 500
//     //     select l.Item1.StopID).ToList();

//     // Console.WriteLine(closeEnough.Count);

//     // foreach (var l in stopsClosestToMe)
//     // {
//     //     Console.WriteLine("Start: " + l);
//     // }
//     // foreach (var l in stopsClosestToDestination)
//     // {
//     //     Console.WriteLine("End:" + l);
//     // }
//     // Console.WriteLine("stops.txt: " + l.Item1.StopID + "\t" + l.Item1.StopName + ":\tDistance:\t" + myLocation.GetDistanceTo(l.Item2));
//     // Console.WriteLine(l);


//     // Stop? s = await context.Stops.Any<Stop>();
//     // List<Stop> stopQuery = context.Stops.ToList();
//     // foreach (var s in stopQuery)
//     // {
//     //     Console.WriteLine(s.StopID + ", " + s.StopName + ", " + s.StopLat + ", " + s.StopLon);
//     // }



//     // IEnumerable<Stop> stopRecords;
//     // List<Stop> stopLists;




//     // List<(Stop, GeoCoordinate)> closeEnough;
//     // List<string> stopIDs;
//     // List<char> letters;


//     /*
//         COMPLETED DATA ADDING:
//         stops.txt
//         stop_times.txt
//         trips.txt
//         calendar.txt
//         calendar_dates.txt
//         routes.txt
//     */


//     // using (var reader = new StreamReader("./gtfs_csv/routes.txt"))
//     // using (var csv = new CsvReader(reader, config))
//     // {
//     //     var records = csv.GetRecords<RouteGTFS>();

//     //     var recList = (from r in records select r).ToList();
//     //     foreach (RouteGTFS s in recList)
//     //     {
//     //         context.RouteGTFSs.Add(s);
//     //     }

//     //     await context.SaveChangesAsync();
//     // }

// }


// GeoCoordinate myLocation = new GeoCoordinate(40.831788646900975, -73.91836083149104);
// letters =
//     (from l in stopLists
//     where l.Item2.GetDistanceTo(myLocation) <= 500
//     group l by l.Item1.StopID[0] into grouped
//     select grouped.First().Item1.StopID[0]).ToList();

// closeEnough =
//     (from l in stopLists
//     where l.Item2.GetDistanceTo(myLocation) <= 500
//     select l).ToList();
// stopIDs =
//     (from l in stopLists
//     where l.Item2.GetDistanceTo(myLocation) <= 500
//     select l.Item1.StopID).ToList();

// // Console.WriteLine(closeEnough.Count);

// foreach (var l in letters)
// {
//     Console.WriteLine("stops.txt: " + l);
//     // Console.WriteLine("stops.txt: " + l.Item1.StopID + "\t" + l.Item1.StopName + ":\tDistance:\t" + myLocation.GetDistanceTo(l.Item2));
//     // Console.WriteLine(l);
// }

// foreach (Stop stop in records)
// {
//     Console.WriteLine(stop.StopID + "\t" + stop.StopName + ":\t" + stop.StopLat.ToString() + ", " + stop.StopLon.ToString());
// }}



app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
