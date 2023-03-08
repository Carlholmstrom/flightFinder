using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using flightFinder.API.Models;

namespace flightFinder.API.Data;

public static class FlightDataImporter
{
    public static void ImportData(FlightDbContext context)
    {
        var json = File.ReadAllText("./Data/data.json");
        var flightRoutes = JsonConvert.DeserializeObject<List<FlightRoute>>(json);

        foreach (var flightRoute in flightRoutes)
        {
            var existingFlightRoute = context.FlightRoutes.FirstOrDefault(fr => fr.RouteId == flightRoute.RouteId);

            if (existingFlightRoute == null)
            {
                context.FlightRoutes.Add(flightRoute);
            }
            else
            {
                existingFlightRoute.DepartureDestination = flightRoute.DepartureDestination;
                existingFlightRoute.ArrivalDestination = flightRoute.ArrivalDestination;

                foreach (var flight in flightRoute.Itineraries)
                {
                    var existingFlight = existingFlightRoute.Itineraries.FirstOrDefault(f => f.FlightId == flight.FlightId);

                    if (existingFlight == null)
                    {
                        existingFlightRoute.Itineraries.Add(flight);
                    }
                    else
                    {
                        existingFlight.DepartureAt = flight.DepartureAt;
                        existingFlight.ArrivalAt = flight.ArrivalAt;
                        existingFlight.AvailableSeats = flight.AvailableSeats;
                        existingFlight.Prices = flight.Prices;
                    }
                }
            }
        }

        context.SaveChanges();
    }
}