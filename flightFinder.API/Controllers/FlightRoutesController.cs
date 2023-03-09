using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flightFinder.API.Data;
using flightFinder.API.DTOs.Outgoing;
using flightFinder.API.Interfaces;
using flightFinder.API.Models;

namespace flightFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightRoutesController : ControllerBase
    {
        private readonly IFlightRouteRepository _flightRouteRepository;
        private readonly IMapper _mapper;

        public FlightRoutesController(IFlightRouteRepository flightRouteRepository, IMapper mapper)
        {
            _flightRouteRepository = flightRouteRepository;
            _mapper = mapper;
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
        public async Task<ActionResult<IEnumerable<FlightRouteDto>>> GetFlightsByRoute(string departureDestination, string arrivalDestination, DateTime departureTime, DateTime arrivalTime)
        {
            var routes = await _flightRouteRepository.SearchFlightRoutesAsync(departureDestination, arrivalDestination, departureTime, arrivalTime);

            var result = _mapper.Map<IEnumerable<FlightRouteDto>>(routes);

            return Ok(result);
        }
        
        


    }
}
