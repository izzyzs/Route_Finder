using Microsoft.EntityFrameworkCore;

namespace API.Models;

// using System.Collections;

[PrimaryKey(nameof(ServiceID))]
public class CalendarGTFS
{
    // public Calendar(DateTime d)
    // {
    //     d.Day
    // }

    public required string ServiceID { get; set; }
    public int Monday { get; set; }
    public int Tuesday { get; set; }
    public int Wednesday { get; set; }
    public int Thursday { get; set; }
    public int Friday { get; set; }
    public int Saturday { get; set; }
    public int Sunday { get; set; }

    public bool IsWeekday
    {
        get {

            return this.Monday == 1 || this.Tuesday == 1 || this.Wednesday == 1 || this.Thursday == 1 || this.Friday == 1;
        }
    }

    public required string StartDate { get; set; }
    public required string EndDate { get; set; }
    public int Day(int day)
    {
        switch (day)
        {
            case 0:
                return this.Sunday;
            case 1:
                return this.Monday;
            case 2:
                return this.Tuesday;
            case 3:
                return this.Wednesday;
            case 4:
                return this.Thursday;
            case 5:
                return this.Friday;
            case 6:
                return this.Saturday;
            default:
                return -1;
        }
    }
}