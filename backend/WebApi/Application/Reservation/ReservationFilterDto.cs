namespace Application.Reservations;

public class ReservationFilterDto
{
    public int? PropertyId { get; init; }
    public string? GuestName { get; init; }
    public DateOnly? DateFrom { get; init; }
    public DateOnly? DateTo { get; init; }
}
