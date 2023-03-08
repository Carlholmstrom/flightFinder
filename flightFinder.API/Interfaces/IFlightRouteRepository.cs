using flightFinder.API.Models;

namespace flightFinder.API.Interfaces;

public interface IFlightRouteRepository
{
    Task<IEnumerable<FlightRoute>> GetAllAsync();

    Task<FlightRoute> GetAsync(string id);
    bool FlightRouteExists(string id);
}