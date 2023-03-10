namespace flightFinder.API.DTOs.Outgoing;

public class FlightWithLayoverDto
{
    public string FlightId { get; set; }
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public int AvailableSeats { get; set; }
   // public PriceDto Prices { get; set; }
    public FlightRouteDto FlightRoute { get; set; }
    public string TotalTravelTime => (ArrivalAt - DepartureAt).ToString(@"h\hmm\m");
    
}
