namespace Application.Reservations;

public class ReservationFilterDto
{
    public int? PropertyId { get; set; }
    public string? GuestName { get; set; }
    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
}
