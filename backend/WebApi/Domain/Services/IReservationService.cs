using Domain.Entities;
using Domain.Filters;

namespace Domain.Services;

public interface IReservationService
{
    Task<Reservation> CreateAsync( Reservation reservation, CancellationToken ct = default );
    Task<Reservation?> GetByIdAsync( int id, CancellationToken ct = default );
    Task<IEnumerable<Reservation>> GetListAsync( ReservationFilter filter, CancellationToken ct = default );
    Task CancelAsync( int id, CancellationToken ct = default );
}
