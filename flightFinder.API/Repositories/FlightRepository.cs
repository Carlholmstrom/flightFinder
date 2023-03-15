using flightFinder.API.Data;
using flightFinder.API.DTOs.Outgoing;
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
            .Include(f => f.Prices)
            .Where(f => f.FlightRoute.DepartureDestination == departureDestination
                        && f.FlightRoute.ArrivalDestination == arrivalDestination
                        && f.DepartureAt.Date == date.Date
                        && f.AvailableSeats > 0)
            .ToListAsync();

        var flightPlan = new List<Flight>();
        foreach (var flight in flights)
        {
            flightPlan.Add(new Flight
            {
                FlightId = flight.FlightId,
                //DepartureDestination = flight.FlightRoute.DepartureDestination,
                DepartureAt = flight.DepartureAt,
                //ArrivalDestination = flight.FlightRoute.ArrivalDestination,
                ArrivalAt = flight.ArrivalAt,
                AvailableSeats = flight.AvailableSeats,
                Prices = new Price
                {
                    Currency = flight.Prices.Currency, 
                    Adult = flight.Prices.Adult,
                    Child = flight.Prices.Child
                },
                FlightRoute = new FlightRoute
                {
                    DepartureDestination = departureDestination,
                    ArrivalDestination = arrivalDestination
                }
               

            });
        }
  
        return flightPlan;
    }

    public async Task<Flight> GetAsync(string id)
    {
        return await _context.Flights.FirstOrDefaultAsync(x => x.FlightId == id);
    }

    public async Task<IEnumerable<Flight>> GetFlightsByRouteWithLayoverAsync(string departureDestination, string arrivalDestination, DateTime date)
{
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
        var flightsToLayover = await _context.Flights
            .Include(p => p.Prices)
            .Include(f => f.FlightRoute)
            .Where(f => f.FlightRoute.DepartureDestination == departureDestination
                        && f.FlightRoute.ArrivalDestination == layoverDestination
                        && f.DepartureAt.Date == date.Date)
            
            .ToListAsync();
        
        var flightsFromLayover = await _context.Flights
            .Include(p => p.Prices)
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
                  var totalPrice = flightToLayover.Prices.Adult + flightToLayover.Prices.Child 
                                   + flightFromLayover.Prices.Adult + flightFromLayover.Prices.Child;
      
                  flightsWithLayover.Add(new Flight
                  {
                      FlightId = $"{flightToLayover.FlightId} & {flightFromLayover.FlightId}",
                      DepartureAt = flightToLayover.DepartureAt,
                      ArrivalAt = flightFromLayover.ArrivalAt,
                      AvailableSeats = Math.Min(flightToLayover.AvailableSeats, flightFromLayover.AvailableSeats),
                      
                      Prices = new Price
                      {
                          Currency = flightToLayover.Prices.Currency, 
                          Adult = flightToLayover.Prices.Adult,
                          Child = flightToLayover.Prices.Child
                      },
                      
                      FlightRoute = new FlightRoute
                      {
                          DepartureDestination = flightToLayover.FlightRoute.DepartureDestination,
                          ArrivalDestination = flightFromLayover.FlightRoute.ArrivalDestination,
                          LayoverDestination = layoverDestination,
                          LayoverDuration = layoverDuration,
                          TotalPrice = totalPrice
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

    public bool FlightExists(string id)
    {
        return _context.Flights.Any(x => x.FlightId == id);
    }
    

}