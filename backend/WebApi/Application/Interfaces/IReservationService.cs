using Application.Reservations;

namespace Application.Interfaces;

public interface IReservationService
{
    Task<int> CreateAsync( CreateReservationDto dto, CancellationToken ct = default );
    Task<ReservationDto?> GetByIdAsync( int id, CancellationToken ct = default );
    Task<IReadOnlyList<ReservationDto>> GetListAsync( ReservationFilterDto filter, CancellationToken ct = default );
    Task CancelAsync( int id, CancellationToken ct = default );
}
