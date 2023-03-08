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
    }
}
