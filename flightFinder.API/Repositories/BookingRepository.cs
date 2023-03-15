using flightFinder.API.Data;
using flightFinder.API.DTOs.Incoming;
using flightFinder.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace flightFinder.API.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly FlightDbContext _context;
    public BookingRepository(FlightDbContext context)
    {
        _context = context;
    }
    public async Task<bool> CreateBooking(IncomingBookingDto incomingBooking)
    {
        var bookingRequestFlights = await _context.Flights.Where(x => incomingBooking.FlightNumbers.Contains(x.FlightId)).ToListAsync();
        foreach(var flight in bookingRequestFlights){
            flight.AvailableSeats -= incomingBooking.Seats;
            _context.Entry(flight).State = EntityState.Modified;
        }
        return await Save()? true : false;
    }
    public async Task<bool> SeatsAvailableAsync(string flightNumber , int numberOfSeats) {
        var availableSeats = await _context.Flights.Where(x =>x.FlightId == flightNumber).Select( x => x.AvailableSeats).FirstOrDefaultAsync();
        if (numberOfSeats < availableSeats){
            return true;
        }
        return false;
    }
    public async Task<bool> Save()
    {
        var saved =await _context.SaveChangesAsync();
        return saved > 0 ? true : false;
    }
}