namespace flightFinder.API.DTOs.Outgoing;

public class FlightRouteDto
{
    public string DepartureDestination { get; set; }
    public string ArrivalDestination { get; set; }
    public List<FlightDto> Itineraries { get; set; }
}