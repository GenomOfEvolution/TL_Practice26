using Application.Reservations;

namespace Application.Interfaces;

public interface IReservationService
{
    Task<int> CreateAsync( CreateReservationDto dto, CancellationToken ct );
    Task<ReservationDto?> GetByIdAsync( int id, CancellationToken ct );
    Task<IReadOnlyList<ReservationDto>> GetListAsync( ReservationFilterDto filter, CancellationToken ct );
    Task CancelAsync( int id, CancellationToken ct );
}
