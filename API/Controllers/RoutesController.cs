using System.Text.Json;
using Newtonsoft.Json.Schema;
using API.Data;
using API.Json;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RoutesController : ControllerBase
{
    private readonly GTFSDbContext  _context;
    private readonly LocationService _locationService;

    public RoutesController(GTFSDbContext context, LocationService locationService)
    {
        _context = context;
        _locationService = locationService;
    }

    [HttpPost]
    public async void /*Task<ActionResult<IEnumerable<DestinationRoute>>>*/ getRoutesNow([FromBody] JsonElement body)
    {
        string schemaJson = 
        @"{
            '$schema': 'https://json-schema.org/draft/2020-12/schema',
            'title': 'TransitDetails',
            'description': 'includes the folowing travel details: starting and ending geocoordinates and travel time as a string',
            'type': 'object',
            'properties': {
                'startLat': { 'type': 'number' },
                'startLon': { 'type': 'number' },
                'endLat': { 'type': 'number' },
                'endLon': { 'type': 'number' },
                'travelDateTime': { 'type': 'string' },
            }
            'required': [startLat, startLon, endLat, endLon, travelDateTime]
        }";


        JSchema schema = JSchema.Parse(schemaJson);
        JObject jsonData = JObject.Parse(body.ToString());

        if(!jsonData.IsValid(schema, out IList<string> errors))
        {
            // return BadRequest(new { message = "Validation failed", errors });
        }

        FindRouteJson? routeJson = JsonConvert.DeserializeObject<FindRouteJson>(jsonData.ToString());
        if (routeJson == null)
        {
            // return BadRequest(new { message = "Failed JSON conversion"});
        }

        DestinationRoute r = new DestinationRoute(_locationService, _context, routeJson.StartLat, routeJson.StartLon, routeJson.EndLat, routeJson.EndLon, routeJson.TravelDateTime);
        List<string> startStops, endStops;
        (startStops, endStops) = await r.FindClosestStops();

        /*
        *   Return destination routes based on shortest travel times *   based on inputted start location and time
        *   1) based on DateTime find corresponding schedule
        *   2) based on traveltime
        */
    }

    // [HttpGet("lat={lat}&lon={lon}&time={time}&travel_time={travelTime}")]
    // public async Task<ActionResult<IEnumerable<DestinationRoute>>> getRoutesForTime(double lat, double lon, string time, string travelTime)
    // {
    //     /*
    //     *   Return destination routes based on shortest travel times *   based on inputted start location and time
    //     *   1) based on DateTime find corresponding schedule
    //     *   2) based on traveltime
    //     */
    // }
}