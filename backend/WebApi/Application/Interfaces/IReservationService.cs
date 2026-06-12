using Application.Reservations;

namespace Application.Interfaces;

public interface IReservationService
{
    Task<int> CreateAsync( CreateReservationDto dto, CancellationToken ct );
    Task<ReservationDto> GetByIdAsync( int reservationId, CancellationToken ct );
    Task<IReadOnlyList<ReservationDto>> GetAllAsync( ReservationFilterDto filter, CancellationToken ct );
    Task CancelAsync( int reservationId, CancellationToken ct );
}
