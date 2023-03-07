namespace flightFinder.API.Models;

public class Flight
{
    public string FlightId { get; set; }
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public int AvailableSeats { get; set; }
    public Price Prices { get; set; }
}