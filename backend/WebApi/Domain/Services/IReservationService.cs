using Domain.Entities;
using Domain.Filters;

namespace Domain.Services;

public interface IReservationService
{
    Task<int> CreateAsync( Reservation reservation, CancellationToken ct = default );
    Task<Reservation?> GetByIdAsync( int id, CancellationToken ct = default );
    Task<IReadOnlyList<Reservation>> GetListAsync( ReservationFilter filter, CancellationToken ct = default );
    Task CancelAsync( int id, CancellationToken ct = default );
}
