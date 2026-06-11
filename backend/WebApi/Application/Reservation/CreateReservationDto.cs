namespace Application.Reservations;

public class CreateReservationDto
{
    public int PropertyId { get; set; }
    public int RoomTypeId { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public DateOnly DepartureDate { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public string GuestPhoneNumber { get; set; } = string.Empty;
}
