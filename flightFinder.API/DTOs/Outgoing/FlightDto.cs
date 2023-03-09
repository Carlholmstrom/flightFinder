namespace flightFinder.API.DTOs.Outgoing;

public class FlightDto
{
    public string FlightId { get; set; }
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public int AvailableSeats { get; set; }
    public string FlightTime => (ArrivalAt - DepartureAt).ToString(@"h\hmm\m");
    public PriceDto Prices { get; set; }
}


