using Application.Reservations;
using Domain.Entities;

namespace Application.Mappers;

public static class CreateReservationDtoToEntityMapper
{
    public static Domain.Entities.Reservation Map( CreateReservationDto dto )
    {
        return new Domain.Entities.Reservation
        {
            PropertyId = dto.PropertyId,
            RoomTypeId = dto.RoomTypeId,
            ArrivalDate = dto.ArrivalDate,
            ArrivalTime = dto.ArrivalTime,
            DepartureDate = dto.DepartureDate,
            DepartureTime = dto.DepartureTime,
            GuestName = dto.GuestName,
            GuestPhoneNumber = dto.GuestPhoneNumber,
        };
    }
}
