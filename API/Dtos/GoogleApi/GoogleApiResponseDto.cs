namespace API.Dtos.GoogleApi;




public class GoogleApiResponseDto
{
    public List<RouteDto> Routes { get; set; } = new();

    
}

public class RouteDto
{
    public List<LegDto> Legs { get; set; } = new();
}

public class LegDto
{
    public List<StepDto> Steps { get; set; } = new();

    public bool Sanitize()
    {
        for (int i = Steps.Count - 1; i >= 0; i--)
        {
            if (!Steps[i].isValid())
            {
                Steps.RemoveAt(i);
            }
        }

        // validation check; make sure everyone is valid
        foreach (StepDto step in Steps)
        {
            if (!step.isValid())
            {
                return false;
            }
        }
        return true;
    }
}

public class StepDto
{
    public TransitDetailsDto? TransitDetails { get; set; }
    public bool isValid()
    {
        return TransitDetails != null;
    }
}

public class TransitDetailsDto
{
    public StopDetailsDto StopDetails { get; set; } = null!;
    public LocalizedValuesDto LocalizedValues { get; set; } = null!;
    public string Headsign { get; set; } = null!;
    public TransitLineDto TransitLine { get; set; } = null!;
    public int StopCount { get; set; }

    
}

public class StopDetailsDto
{
    public StopDto ArrivalStop { get; set; } = null!;
    public string ArrivalTime { get; set; } = null!;
    public StopDto DepartureStop { get; set; } = null!;
    public string DepartureTime { get; set; } = null!;
}

public class StopDto
{
    public string Name { get; set; } = null!;
    public LocationDto Location { get; set; } = null!;
}

public class LocationDto
{
    public LatLngDto LatLng { get; set; } = null!;
}

public class LatLngDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class LocalizedValuesDto
{
    public LocalizedTimeDto ArrivalTime { get; set; } = null!;
    public LocalizedTimeDto DepartureTime { get; set; } = null!;
}

public class LocalizedTimeDto
{
    public TimeTextDto Time { get; set; } = null!;
    public string TimeZone { get; set; } = null!;
}

public class TimeTextDto
{
    public string Text { get; set; } = null!;
}

public class TransitLineDto
{
    public List<AgencyDto> Agencies { get; set; } = new();
    public string Name { get; set; } = null!;
    public string Color { get; set; } = null!;
    public string NameShort { get; set; } = null!;
    public string TextColor { get; set; } = null!;
    public VehicleDto Vehicle { get; set; } = null!;

}

public class AgencyDto
{
    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Uri { get; set; } = null!;

}

public class VehicleDto
{
    public VehicleNameDto Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string IconUri { get; set; } = null!;
}

public class VehicleNameDto
{
    public string Text { get; set; } = null!;
}