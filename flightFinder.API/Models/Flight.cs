using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flightFinder.API.Models;

public class Flight
{
   [Key]
    public string FlightId { get; set; }
    public DateTime DepartureAt { get; set; }
    public DateTime ArrivalAt { get; set; }
    [NotMapped]
    public string DepartureDestination { get; set; }
    [NotMapped]
    public string ArrivalDestination { get; set; }
    public int AvailableSeats { get; set; }
    public Price Prices { get; set; }
    public FlightRoute FlightRoute { get; set; }
    
}