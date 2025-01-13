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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GTFSDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GTFSDbContext>();
    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.Underscore(),
    };

    GeoCoordinate destination = new GeoCoordinate(40.823206385675476, -73.93912865038058);
    

    List<(Stop, GeoCoordinate)> closeEnough;
    List<string> stopsClosestToMe;
    List<string> stopsClosestToDestination;
    List<char> letters;
    
    double myLat = 40.831788646900975;
    double myLon = -73.91836083149104;
    double destinationLat = 40.823826746298984;
    double destinationLon = -73.93944955029748;
    // List<(Stop, GeoCoordinate)> locations =
    //     (from r in context.Stops
    //      select (r, new GeoCoordinate(r.StopLat, r.StopLon))).ToList(); 

    List<Location> locations = context.Stops.Select(s => new Location(s, new GeoCoordinate(s.StopLat, s.StopLon))).ToList();
    
    stopsClosestToMe = DestinationRoute.FindClosestStops(locations, myLat, myLon);
    stopsClosestToDestination = DestinationRoute.FindClosestStops(locations, destinationLat, destinationLon);

    // closeEnough =
    //     (from l in locationsList
    //     where l.Item2.GetDistanceTo(myLocation) <= 500
    //     select l).ToList();
    // stopsClosestToMe =
    //     (from l in locationsList
    //     where l.Item2.GetDistanceTo(myLocation) <= 500
    //     select l.Item1.StopID).ToList();
    
    // Console.WriteLine(closeEnough.Count);

    foreach (var l in stopsClosestToMe)
    {
        Console.WriteLine("Start: " + l);
    }
    foreach (var l in stopsClosestToDestination)
    {
        Console.WriteLine("End:" + l);
    }
        // Console.WriteLine("stops.txt: " + l.Item1.StopID + "\t" + l.Item1.StopName + ":\tDistance:\t" + myLocation.GetDistanceTo(l.Item2));
        // Console.WriteLine(l);
    

    // Stop? s = await context.Stops.Any<Stop>();
    // List<Stop> stopQuery = context.Stops.ToList();
    // foreach (var s in stopQuery)
    // {
    //     Console.WriteLine(s.StopID + ", " + s.StopName + ", " + s.StopLat + ", " + s.StopLon);
    // }



    // IEnumerable<Stop> stopRecords;
    // List<Stop> stopLists;




    // List<(Stop, GeoCoordinate)> closeEnough;
    // List<string> stopIDs;
    // List<char> letters;


    /*
        COMPLETED DATA ADDING:
        stops.txt
        stop_times.txt
        trips.txt
        calendar.txt
        calendar_dates.txt
        routes.txt
    */


    // using (var reader = new StreamReader("./gtfs_csv/routes.txt"))
    // using (var csv = new CsvReader(reader, config))
    // {
    //     var records = csv.GetRecords<RouteGTFS>();

    //     var recList = (from r in records select r).ToList();
    //     foreach (RouteGTFS s in recList)
    //     {
    //         context.RouteGTFSs.Add(s);
    //     }

    //     await context.SaveChangesAsync();
    // }
        
}


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







app.MapGet("/", () => "Hello World!");

app.Run();
