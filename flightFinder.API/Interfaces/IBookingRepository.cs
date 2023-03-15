using flightFinder.API.DTOs.Incoming;

namespace flightFinder.API.Interfaces;

public interface IBookingRepository
{
    Task<bool> CreateBooking(IncomingBookingDto incomingBooking);
    Task<bool> SeatsAvailableAsync(string flightNumber , int numberOfSeats);
}