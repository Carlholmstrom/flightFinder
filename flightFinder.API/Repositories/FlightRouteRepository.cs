using flightFinder.API.Data;
using flightFinder.API.Interfaces;
using flightFinder.API.Models;
using Microsoft.EntityFrameworkCore;

namespace flightFinder.API.Repositories;

public class FlightRouteRepository: IFlightRouteRepository
{
    private readonly FlightDbContext _context;

    public FlightRouteRepository(FlightDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<FlightRoute>> GetAllAsync()
    {
        return await _context.FlightRoutes.ToListAsync();
    }

    public async Task<IEnumerable<FlightRoute>> SearchFlightRoutesAsync(string departureDestination, string arrivalDestination)
    {
        if (string.IsNullOrEmpty(departureDestination))
        {
            throw new ArgumentException("Departure destination cannot be null or empty", nameof(departureDestination));
        }

        if (string.IsNullOrEmpty(arrivalDestination))
        {
            throw new ArgumentException("Arrival destination cannot be null or empty", nameof(arrivalDestination));
        }

        return await _context.FlightRoutes
            .Include(fr => fr.Itineraries)
            .ThenInclude(i => i.Prices)
            .Where(fr => fr.DepartureDestination.ToUpper() == departureDestination.ToUpper()
                         && fr.ArrivalDestination.ToUpper() == arrivalDestination.ToUpper())
            .ToListAsync();
    }





    public async Task<FlightRoute> GetAsync(string id)
    {
        return await _context.FlightRoutes.FirstOrDefaultAsync(x => x.RouteId == id);
    }

    public bool FlightRouteExists(string id)
    {
        return _context.FlightRoutes.Any(x => x.RouteId == id);
    }
}