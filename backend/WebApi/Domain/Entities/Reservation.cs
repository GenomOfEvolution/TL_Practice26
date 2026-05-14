namespace Domain.Entities;

/// <summary>
/// Бронирование
/// </summary>
public class Reservation
{
    public int Id { get; private init; }
    public int PropertyId { get; set; }
    public int RoomTypeId { get; set; }

    public DateOnly ArrivalDate { get; set; }
    public TimeOnly ArrivalTime { get; set; }

    public DateOnly DepartureDate { get; set; }
    public TimeOnly DepartureTime { get; set; }

    public string GuestName { get; set; } = String.Empty;
    public string GuestPhoneNumber { get; set; } = String.Empty;

    public decimal Total { get; set; }
    public Currency Currency { get; set; }
}
