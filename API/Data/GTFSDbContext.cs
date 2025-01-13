using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class GTFSDbContext : DbContext
    {
        public GTFSDbContext(DbContextOptions<GTFSDbContext> options) : base(options)
        {       
        }

        public DbSet<CalendarDate> CalendarDates => Set<CalendarDate>();
        public DbSet<CalendarGTFS> CalendarGTFSs => Set<CalendarGTFS>();
        public DbSet<RouteGTFS> RouteGTFSs => Set<RouteGTFS>();
        public DbSet<Stop> Stops => Set<Stop>();
        public DbSet<StopTime> StopTimes => Set<StopTime>();
        public DbSet<Trip> Trips => Set<Trip>();
    }
}