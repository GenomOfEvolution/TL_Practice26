namespace Domain.Filters;

public record SearchFilter
(
    string City,
    DateOnly ArrivalDate,
    DateOnly DepartureDate,
    int Guests,
    decimal? MaxPrice = null
);
