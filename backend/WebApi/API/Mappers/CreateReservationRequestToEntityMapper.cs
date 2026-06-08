using API.DTO;
using Domain.Entities;

namespace API.Mappers;

public static class CreateReservationRequestToEntityMapper
{
    public static Reservation Map( CreateReservationRequest request )
    {
        return new Reservation
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
