namespace API.DTO;

public class SearchRequest
{
    public string? City { get; init; }
    public DateOnly? ArrivalDate { get; init; }
    public DateOnly? DepartureDate { get; init; }
    public int? Guests { get; init; }
    public decimal? MaxPrice { get; init; }
}
