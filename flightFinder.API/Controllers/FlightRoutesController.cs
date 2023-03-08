using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flightFinder.API.Data;
using flightFinder.API.Interfaces;
using flightFinder.API.Models;

namespace flightFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightRoutesController : ControllerBase
    {
        private readonly IFlightRouteRepository _flightRouteRepository;

        public FlightRoutesController(IFlightRouteRepository flightRouteRepository)
        {
            _flightRouteRepository = flightRouteRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<FlightRoute>> GetFlightRoutes()
        {
            return await _flightRouteRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FlightRoute>> GetFlightRoute(string id)
        {
            var flightRoute = await _flightRouteRepository.GetAsync(id);

            if (flightRoute == null)
            {
                return NotFound();
            }

            return flightRoute;
        }
       [HttpGet("{departureDestination}/{arrivalDestination}")]
       public async Task<ActionResult<IEnumerable<object>>> GetFlightsByRoute(string departureDestination, string arrivalDestination)
       {
           var routes = await _flightRouteRepository.SearchFlightRoutesAsync(departureDestination, arrivalDestination);
       
           var result = routes.Select(r => new
           {
               route_id = r.RouteId,
               departureDestination = r.DepartureDestination,
               arrivalDestination = r.ArrivalDestination,
               itineraries = r.Itineraries.Where(f => f != null && f.AvailableSeats >= 1).Select(f => new
               {
                   flight_id = f.FlightId,
                   departureAt = f.DepartureAt,
                   arrivalAt = f.ArrivalAt,
                   availableSeats = f.AvailableSeats,
                   prices = new
                   {
                       currency = f.Prices.Currency,
                       adult = f.Prices.Adult,
                       child = f.Prices.Child
                   }
               })
           });
       
           return Ok(result);
       }


    }
}
