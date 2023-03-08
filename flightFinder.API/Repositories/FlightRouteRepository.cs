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

    public async Task<FlightRoute> GetAsync(string id)
    {
        return await _context.FlightRoutes.FirstOrDefaultAsync(x => x.RouteId == id);
    }

    public bool FlightRouteExists(string id)
    {
        return _context.FlightRoutes.Any(x => x.RouteId == id);
    }
}