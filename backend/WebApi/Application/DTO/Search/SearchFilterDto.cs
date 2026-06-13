namespace Application.DTO.Search;

public class SearchFilterDto
{
    public string? City { get; init; }
    public DateOnly? ArrivalDate { get; init; }
    public DateOnly? DepartureDate { get; init; }
    public int? Guests { get; init; }
    public decimal? MaxPrice { get; init; }
}
