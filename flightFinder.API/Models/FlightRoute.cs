using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace flightFinder.API.Models;

public class FlightRoute
{
   [Key]
    public string RouteId { get; set; }
    public string DepartureDestination { get; set; }
    public string ArrivalDestination { get; set; }
    [NotMapped]
    public string LayoverDestination { get; set; }
    [NotMapped]
    public TimeSpan LayoverDuration { get; set; }
    [NotMapped]
    public decimal TotalPrice { get; set; }

    public List<Flight> Itineraries { get; set; }
    
}