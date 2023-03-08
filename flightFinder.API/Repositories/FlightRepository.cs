using flightFinder.API.Data;
using flightFinder.API.Interfaces;
using flightFinder.API.Models;
using Microsoft.EntityFrameworkCore;

namespace flightFinder.API.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly FlightDbContext _context;

    public FlightRepository(FlightDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Flight>> GetAllAsync()
    {
        return await _context.Flights.ToListAsync();
    }

    public async Task<IEnumerable<Flight>> GetFlightsByRouteAsync(string departureDestination,
        string arrivalDestination)
    {
        return await _context.Flights
            .Where(f => f.FlightRoute.DepartureDestination == departureDestination
                        && f.FlightRoute.ArrivalDestination == arrivalDestination)
            .ToListAsync();
    }
    
    public async Task<Flight> GetAsync(string id)
    {
        return await _context.Flights.FirstOrDefaultAsync(x => x.FlightId == id);
    }

    public bool FlightExists(string id)
    {
        return _context.Flights.Any(x => x.FlightId == id);
    }
}