using System.ComponentModel.DataAnnotations;

namespace flightFinder.API.DTOs.Incoming;

public class IncomingBookingDto
{
    [Required]
    [Range(1, 1000, ErrorMessage = "The number of seats must be at least 1.")]
    public int Seats { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    [Required]
    public List<string> FlightNumbers { get; set; }
}