namespace API.Models;

public class Leg // start_time, end_time, start_stop_id, end_stop_id
{
    public required StopTime StartTime;
    public required StopTime EndTime;
    public required Stop StartStopID;
    public required Stop EndStopID;
}