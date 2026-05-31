namespace API.DTO;

public class CreateReservationRequest
{
    public int PropertyId { get; set; }
    public int RoomTypeId { get; set; }

    public DateOnly ArrivalDate { get; set; }
    public TimeOnly ArrivalTime { get; set; }

    public DateOnly DepartureDate { get; set; }
    public TimeOnly DepartureTime { get; set; }

    public string GuestName { get; set; } = String.Empty;
    public string GuestPhoneNumber { get; set; } = String.Empty;
}
