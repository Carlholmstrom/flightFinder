using flightFinder.API.Models;

namespace flightFinder.API.Interfaces;

public interface IFlightRepository
{
    Task<IEnumerable<Flight>> GetAllAsync();

    Task<Flight> GetAsync(string id);
    bool FlightExists(string id);
}