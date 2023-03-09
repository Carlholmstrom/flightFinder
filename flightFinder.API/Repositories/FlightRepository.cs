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

    public async Task<IEnumerable<Flight>> GetFlightsByRouteAsync(string departureDestination, string arrivalDestination, DateTime date)
    {
        var flights = await _context.Flights
            .Include(f => f.FlightRoute)
            .Where(f => f.FlightRoute.DepartureDestination == departureDestination
                        && f.FlightRoute.ArrivalDestination == arrivalDestination
                        && f.DepartureAt.Date == date.Date
                        && f.AvailableSeats > 0)
            .ToListAsync();
        return flights;
    }
    
    public async Task<Flight> GetAsync(string id)
    {
        return await _context.Flights.FirstOrDefaultAsync(x => x.FlightId == id);
    }

    public bool FlightExists(string id)
    {
        return _context.Flights.Any(x => x.FlightId == id);
    }
public async Task<IEnumerable<Flight>> GetFlightsByRouteWithLayoverAsync(string departureDestination, string arrivalDestination, DateTime date)
{
    // Find all possible layover destinations based on departure and arrival cities
    var possibleLayoverDestinations = await _context.FlightRoutes
        .Where(fr => fr.DepartureDestination == departureDestination)
        .Join(
            _context.FlightRoutes.Where(fr => fr.ArrivalDestination == arrivalDestination),
            fr => fr.ArrivalDestination,
            fr => fr.DepartureDestination,
            (fr1, fr2) => fr1.ArrivalDestination
        )
        .Distinct()
        .ToListAsync();
    var flightsWithLayover = new List<Flight>();
    foreach (var layoverDestination in possibleLayoverDestinations)
    {
        // Find all flights that connect the departure city to the layover destination
        var flightsToLayover = await _context.Flights
            .Include(f => f.FlightRoute)
            .Where(f => f.FlightRoute.DepartureDestination == departureDestination
                        && f.FlightRoute.ArrivalDestination == layoverDestination
                        && f.DepartureAt.Date == date.Date)
            .ToListAsync();
        // Find all flights that connect the layover destination to the arrival city
        var flightsFromLayover = await _context.Flights
            .Include(f => f.FlightRoute)
            .Where(f => f.FlightRoute.DepartureDestination == layoverDestination
                        && f.FlightRoute.ArrivalDestination == arrivalDestination
                        && f.DepartureAt.Date == date.Date)
            .ToListAsync();
        foreach (var flightToLayover in flightsToLayover)
        {
            foreach (var flightFromLayover in flightsFromLayover)
            {
                var layoverDuration = flightFromLayover.DepartureAt - flightToLayover.ArrivalAt;
                if (layoverDuration >= TimeSpan.FromHours(1))
                {
                    flightsWithLayover.Add(new Flight
                    {
                        FlightId = $"{flightToLayover.FlightId}_{flightFromLayover.FlightId}",
                        DepartureAt = flightToLayover.DepartureAt,
                        ArrivalAt = flightFromLayover.ArrivalAt,
                        AvailableSeats = Math.Min(flightToLayover.AvailableSeats, flightFromLayover.AvailableSeats),
                        Prices = flightToLayover.Prices, // assuming the prices are the same for both flights
                        FlightRoute = new FlightRoute
                        {
                            DepartureDestination = flightToLayover.FlightRoute.DepartureDestination,
                            ArrivalDestination = flightFromLayover.FlightRoute.ArrivalDestination,
                            //Itineraries = new List<Flight> { flightToLayover, flightFromLayover },
                            LayoverDestination = layoverDestination,
                            LayoverDuration = layoverDuration
                        }
                    });
                }
            }
        }
    }
    // Sort the flights by departure time and take the first 10
    var sortedFlights = flightsWithLayover.OrderBy(f => f.DepartureAt).Take(10);
    return sortedFlights;
}


    

}