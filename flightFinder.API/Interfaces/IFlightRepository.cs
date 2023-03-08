using flightFinder.API.Models;

namespace flightFinder.API.Interfaces;

public interface IFlightRepository
{
    Task<IEnumerable<Flight>> GetAllAsync();

    Task<IEnumerable<Flight>> GetFlightsByRouteAsync(string departureDestination, string arrivalDestination);
    Task<IEnumerable<Flight>> GetFlightsAsync(string departureDestination, string arrivalDestination, DateTime departureTime, DateTime arrivalTime);
    Task<Flight> GetAsync(string id);
    bool FlightExists(string id);
}