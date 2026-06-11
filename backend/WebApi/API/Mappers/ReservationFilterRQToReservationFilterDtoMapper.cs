using API.DTO;
using Application.Reservations;

namespace API.Mappers;

public static class ReservationFilterRQToReservationFilterDtoMapper
{
    public static ReservationFilterDto Map( ReservationFilterRQ request )
    {
        return new ReservationFilterDto
        {
            PropertyId = request.PropertyId,
            GuestName = request.GuestName,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
        };
    }
}
