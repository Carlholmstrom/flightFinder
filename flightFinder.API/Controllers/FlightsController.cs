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
using flightFinder.API.Repositories;

namespace flightFinder.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightRepository _flightRepository;

        public FlightsController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Flight>> GetFlights()
        {
            return await _flightRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(string id)
        {
            var flight = await _flightRepository.GetAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }
        
    }
}