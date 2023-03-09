using flightFinder.API.Models;

namespace flightFinder.API.Interfaces
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<Flight> GetAsync(string id);
        bool FlightExists(string id);

        Task<IEnumerable<Flight>> GetFlightsByRouteAsync(string departureDestination, string arrivalDestination, DateTime date);
        Task<IEnumerable<Flight>> GetFlightsByRouteWithLayoverAsync(string departureDestination, string arrivalDestination, DateTime date);
    }
}