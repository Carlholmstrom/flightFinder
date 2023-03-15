using flightFinder.API.Data;
using flightFinder.API.DTOs.Incoming;
using flightFinder.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace flightFinder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;

    public BookingsController(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }
  
    [HttpPost("Create")]
    public async Task<ActionResult> CreateAsync( IncomingBookingDto incomingBooking )
    {
        foreach (var flightId in incomingBooking.FlightNumbers)
        {
            if(! await _bookingRepository.SeatsAvailableAsync(flightId, incomingBooking.Seats)){
                return NotFound("Not enough available seats where found");
            }
        }
        return await _bookingRepository.CreateBooking(incomingBooking) ? Ok("Your booking has been created") : BadRequest("Something went wrong");
    }

}