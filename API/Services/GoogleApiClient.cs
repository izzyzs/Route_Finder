using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using API.Dtos.GoogleApi;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc.Routing;


public class GoogleApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _googleApiKey;

    public GoogleApiClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("RouteFinder/1.0");

        _googleApiKey = config["GoogleApiKey"];
    }

    public async Task<GoogleApiResponseDto> PostTransitRoute(GeoCoordinate start, GeoCoordinate end)
    {
        var uri = new Uri("https://routes.googleapis.com/directions/v2:computeRoutes");
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        request.Headers.Add("X-Goog-Api-Key", $"{_googleApiKey}");
        request.Headers.Add("X-Goog-FieldMask", "routes.legs.steps.transitDetails");

        var reqBody = new
        {
            origin = new
            {
                location = new
                {
                    latLng = new
                    {
                        latitude = start.Latitude,
                        longitude = start.Longitude
                    }
                }
            },
            destination = new
            {
                location = new
                {
                    latLng = new
                    {
                        latitude = end.Latitude,
                        longitude = end.Longitude
                    }
                }
            },
            travelMode = "TRANSIT",
            computeAlternativeRoutes = true,
            transitPreferences = new
            {
                routingPreference = "LESS_WALKING",
                allowedTravelModes = new[] { "TRAIN" }
            }
        };
        var jsonBody = JsonSerializer.Serialize(reqBody);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        request.Content = content;

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var rawJson = await response.Content.ReadAsStringAsync();
        Console.WriteLine("[DEBUG] Raw JSON:\n" + rawJson);

        

        // Then deserialize
        // var result = JsonSerializer.Deserialize<GoogleApiResponseDto>(rawJson);


        return await response.Content.ReadFromJsonAsync<GoogleApiResponseDto>();
    }
}