namespace flightFinder.API.DTOs.Outgoing;

public class PriceDto
{
    public string Currency { get; set; }
    public decimal Adult { get; set; }
    public decimal Child { get; set; }
}