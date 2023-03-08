using System.ComponentModel.DataAnnotations;

namespace flightFinder.API.Models;

public class FlightRoute
{
   [Key]
    public string RouteId { get; set; }
    public string DepartureDestination { get; set; }
    public string ArrivalDestination { get; set; }
    public List<Flight> Itineraries { get; set; }
    
}