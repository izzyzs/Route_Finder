using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey(nameof(RouteID))]
public class RouteGTFS
{
    public required string AgencyID { get; set; }
    public required string RouteID { get; set; }
    public required string RouteShortName { get; set; }
    public required string RouteLongName { get; set; }
    public int RouteType { get; set; }
    public required string RouteDesc { get; set; }
    public required string RouteUrl { get; set; }
    public required string RouteColor { get; set; }
    public string? RouteTextColor { get; set; }
       
}