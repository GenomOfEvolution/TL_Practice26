using Domain.Enums;

namespace Application.DTO.Reservation;

public class ReservationDto
{
    public int Id { get; init; }
    public int PropertyId { get; init; }
    public int RoomTypeId { get; init; }
    public DateOnly ArrivalDate { get; init; }
    public TimeOnly ArrivalTime { get; init; }
    public DateOnly DepartureDate { get; init; }
    public TimeOnly DepartureTime { get; init; }
    public string GuestName { get; init; } = string.Empty;
    public string GuestPhoneNumber { get; init; } = string.Empty;
    public decimal Total { get; init; }
    public Currency Currency { get; init; }
    public bool IsCanceled { get; init; }
}
