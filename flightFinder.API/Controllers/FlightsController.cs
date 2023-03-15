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
using flightFinder.API.Repositories;

namespace flightFinder.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IMapper _mapper;

        public FlightsController(IFlightRepository flightRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
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
       
        [HttpGet("{departureDestination}/{arrivalDestination}")]
        public async Task<ActionResult<IEnumerable<FlightWithLayoverDto>>> GetFlightsByRouteAsync(string departureDestination, string arrivalDestination,DateTime date)
        {
            var flights = await _flightRepository.GetFlightsByRouteAsync(departureDestination, arrivalDestination, date);

            if (flights.Count() == 0)
            {
                flights = await _flightRepository.GetFlightsByRouteWithLayoverAsync(departureDestination,
                    arrivalDestination, date);
            }
            
            var flightDto = _mapper.Map<IEnumerable<FlightDto>>(flights);
            return Ok(flightDto);
        }

        
    }
}
