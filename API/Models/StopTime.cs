using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey(nameof(TripID), nameof(StopSequence))]
public class StopTime
{
   public required string TripID { get; set; }
   public required string StopID { get; set; }
   public required string ArrivalTime { get; set; }
   public required string DepartureTime { get; set; }
   public string? StopSequence { get; set; }

   public static TimeOnly ToTime(string timeString)
   {
      int[] timeArray = timeString.Split(":").Select(t => (int)double.Parse(t)).ToArray();
      timeArray[0] %= 24;
      return new TimeOnly(timeArray[0], timeArray[1], timeArray[2]);
   }
}