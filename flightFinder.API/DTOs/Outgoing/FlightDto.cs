namespace flightFinder.API.DTOs.Outgoing;

public class FlightDto
{
    public string FlightId { get; set; }
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public int AvailableSeats { get; set; }
    public PriceDto Prices { get; set; }
}