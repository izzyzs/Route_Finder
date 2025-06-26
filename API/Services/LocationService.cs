using API.Data;
using API.Models;
using GeoCoordinatePortable;
using Microsoft.Extensions.Caching.Memory;

namespace API.Services;

public interface ILocationService
{ 
    public Task<List<Location>> GetLocationsAsync();
}

public class LocationService : ILocationService
{
    private readonly GTFSDbContext _context;
    private readonly IMemoryCache _cache;


    public LocationService(GTFSDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    private const string CacheKey = "LocationsCache";
    private readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

    public async Task<List<Location>> GetLocationsAsync()
    {
        if (!_cache.TryGetValue(CacheKey, out List<Location>? locations))
        {
            // List<LocationService>
            locations = await Task.Run(() => _context.Stops
            .Select(s => new Location(s, new GeoCoordinate(s.StopLat, s.StopLon)))
            .ToList());

            _cache.Set(CacheKey, locations, CacheDuration);
        }

        return locations ?? [];
    }
}