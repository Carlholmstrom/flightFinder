namespace flightFinder.API.DTOs.Outgoing;

public class FlightRouteDto
{
    public string DepartureDestination { get; set; }
    public string ArrivalDestination { get; set; }
    public string LayoverDestination { get; set; }
    public TimeSpan LayoverDuration { get; set; }
    public decimal TotalPrice { get; set; }
    public List<FlightDto> Itineraries { get; set; }
}