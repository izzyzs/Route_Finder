using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey(nameof(ServiceID), nameof(Date))]
public class CalendarDate
{
    public required string ServiceID { get; set; }
    public required string Date { get; set; }
    public int ExceptionType { get; set; }
}