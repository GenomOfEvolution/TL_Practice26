namespace API.DTO;

public class ReservationFilterRQ
{
    public int? PropertyId { get; set; }
    public string? GuestName { get; set; }
    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
}
