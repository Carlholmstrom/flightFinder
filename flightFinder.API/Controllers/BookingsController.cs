using flightFinder.API.Data;
using flightFinder.API.DTOs.Incoming;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flightFinder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly FlightDbContext _context;

    public BookingsController(FlightDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> BookFlightsAsync(IncomingBookingDto bookingDto)
    {
        var flights = await _context.Flights.Where(f => bookingDto.FlightNumbers.Contains(f.FlightId)).ToListAsync();
    
        if (flights.Sum(f => f.AvailableSeats) < bookingDto.Seats)
        {
            return BadRequest("Not enough seats available for selected flights.");
        }
    
        return Ok("Booking successful.");
    }

}