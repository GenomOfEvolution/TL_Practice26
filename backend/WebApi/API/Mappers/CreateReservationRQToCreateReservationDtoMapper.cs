using API.DTO;
using Application.Reservations;

namespace API.Mappers;

public static class CreateReservationRQToCreateReservationDtoMapper
{
    public static CreateReservationDto Map( CreateReservationRQ request )
    {
        return new CreateReservationDto
        {
            PropertyId = request.PropertyId,
            RoomTypeId = request.RoomTypeId,
            ArrivalDate = request.ArrivalDate,
            ArrivalTime = request.ArrivalTime,
            DepartureDate = request.DepartureDate,
            DepartureTime = request.DepartureTime,
            GuestName = request.GuestName,
            GuestPhoneNumber = request.GuestPhoneNumber,
        };
    }
}
