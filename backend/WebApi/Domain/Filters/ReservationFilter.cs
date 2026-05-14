namespace Domain.Filters;

public record ReservationFilter
(
    int? PropertyId = null,
    string? GuestName = null,
    DateOnly? DateFrom = null,
    DateOnly? DateTo = null
);
