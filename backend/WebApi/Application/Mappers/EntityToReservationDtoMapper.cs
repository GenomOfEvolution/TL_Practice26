using Application.Reservations;
using Domain.Entities;

namespace Application.Mappers;

public static class EntityToReservationDtoMapper
{
    public static ReservationDto Map( Domain.Entities.Reservation entity )
    {
        return new ReservationDto
        {
            Id = entity.Id,
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
            IsCanceled = entity.IsCanceled,
        };
    }
}
