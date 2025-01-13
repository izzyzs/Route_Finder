using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey(nameof(TripID), nameof(StopSequence))]
public class StopTime
{
    public required string TripID { get; set; }
    public required string StopID { get; set; }

    private TimeSpan _arrivalTime;
    public string ArrivalTime
    {
         get
         {
            return _arrivalTime.ToString();
         }
         set
         {
            var parts = value.Split(":");

            // _arrivalTime = TimeSpan.ParseExact(value, "G", CultureInfo.CurrentCulture);
            _arrivalTime = new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
         }
    }
    private TimeSpan _departureTime;
    public string DepartureTime  {
         get
         {
            return _departureTime.ToString();
         }
         set
         {
            var parts = value.Split(":");

            // _departureTime = TimeSpan.ParseExact(value, "G", CultureInfo.CurrentCulture);
            _departureTime = new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
         }}
    public string? StopSequence  { get; set; }

}