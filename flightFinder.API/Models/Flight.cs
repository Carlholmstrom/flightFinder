using System.ComponentModel.DataAnnotations;

namespace flightFinder.API.Models;

public class Flight
{
   [Key]
    public string FlightId { get; set; }
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    public int AvailableSeats { get; set; }
    public Price Prices { get; set; }
    public FlightRoute FlightRoute { get; set; }
    
}