using API.DTO;
using Domain.Entities;

namespace API.Mappers;

public static class EntityToReservationDtoMapper
{
    public static ReservationDTO Map( Reservation entity )
    {
        return new ReservationDTO
        {
            PropertyId = entity.PropertyId,
            RoomTypeId = entity.RoomTypeId,
            ArrivalDate = entity.ArrivalDate,
            ArrivalTime = entity.ArrivalTime,
            DepartureDate = entity.DepartureDate,
            DepartureTime = entity.DepartureTime,
            GuestName = entity.GuestName,
            GuestPhoneNumber = entity.GuestPhoneNumber,
            Total = entity.Total,
            Currency = entity.Currency,
            IsCanceled = entity.IsCanceled
        };
    }
}
