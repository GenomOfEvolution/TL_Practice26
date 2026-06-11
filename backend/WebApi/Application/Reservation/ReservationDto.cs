using Domain.Enums;

namespace Application.Reservations;

public class ReservationDto
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public int RoomTypeId { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public DateOnly DepartureDate { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public string GuestPhoneNumber { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public Currency Currency { get; set; }
    public bool IsCanceled { get; set; }
}
