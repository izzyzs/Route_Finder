

// using API.Data;
// using Microsoft.AspNetCore.Mvc;

// namespace API.Controllers;

// public class RoutesController : ControllerBase
// {
//     private readonly GTFSDbContext _context;

//     public RoutesController(GTFSDbContext context)
//     {
//         _context = context;
//     }

//     [HttpGet("lat={lat}&lon={lon}&time={time}")]
//     public async Task<ActionResult<IEnumerable<DestinationRoute>>> getRoutesNow(double lat, double lon, string time)
//     {
//         /*
//         *   Return destination routes based on shortest travel times *   based on inputted start location and time
//         *   1) based on DateTime find corresponding schedule
//         *   2) based on traveltime
//         */
//     }

//     [HttpGet("lat={lat}&lon={lon}&time={time}&travel_time={travelTime}")]
//     public async Task<ActionResult<IEnumerable<DestinationRoute>>> getRoutesForTime(double lat, double lon, string time, string travelTime)
//     {
//         /*
//         *   Return destination routes based on shortest travel times *   based on inputted start location and time
//         *   1) based on DateTime find corresponding schedule
//         *   2) based on traveltime
//         */
//     }
// }