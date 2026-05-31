using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Бронирование
/// </summary>
public class Reservation
{
    public int Id { get; init; }
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

    public bool IsCanceled { get; set; }

    public void Update( Reservation reservation )
    {
        PropertyId = reservation.PropertyId;
        RoomTypeId = reservation.RoomTypeId;
        ArrivalDate = reservation.ArrivalDate;
        ArrivalTime = reservation.ArrivalTime;
        DepartureDate = reservation.DepartureDate;
        DepartureTime = reservation.DepartureTime;
        GuestName = reservation.GuestName;
        GuestPhoneNumber = reservation.GuestPhoneNumber;
        Total = reservation.Total;
        Currency = reservation.Currency;
        IsCanceled = reservation.IsCanceled;
    }

    public void SetCanceled( bool isCanceled )
    {
        IsCanceled = isCanceled;
    }
}
