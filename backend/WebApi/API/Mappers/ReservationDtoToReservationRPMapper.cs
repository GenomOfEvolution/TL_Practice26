using API.DTO;
using Application.Reservations;

namespace API.Mappers;

public static class ReservationDtoToReservationRPMapper
{
    public static ReservationRP Map( ReservationDto dto )
    {
        return new ReservationRP
        {
            Id = dto.Id,
            PropertyId = dto.PropertyId,
            RoomTypeId = dto.RoomTypeId,
            ArrivalDate = dto.ArrivalDate,
            ArrivalTime = dto.ArrivalTime,
            DepartureDate = dto.DepartureDate,
            DepartureTime = dto.DepartureTime,
            GuestName = dto.GuestName,
            GuestPhoneNumber = dto.GuestPhoneNumber,
            Total = dto.Total,
            Currency = dto.Currency,
            IsCanceled = dto.IsCanceled,
        };
    }
}
