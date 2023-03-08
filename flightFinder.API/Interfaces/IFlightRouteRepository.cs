using flightFinder.API.Models;

namespace flightFinder.API.Interfaces;

public interface IFlightRouteRepository
{
    Task<IEnumerable<FlightRoute>> GetAllAsync();

   Task<IEnumerable<FlightRoute>> SearchFlightRoutesAsync(string departureDestination,
        string arrivalDestination, DateTime departureTime, DateTime arrivalTime);
    //Task<IEnumerable<FlightRoute>> SearchFlightRoutesAsync(string departureDestination, string arrivalDestination);
    Task<FlightRoute> GetAsync(string id);
    bool FlightRouteExists(string id);
}