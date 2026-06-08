using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface IReservationRepository : IBaseRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetOverlappingAsync( int roomTypeId, DateOnly arrival, DateOnly departure, CancellationToken ct = default );
    Task<IEnumerable<Reservation>> GetByFiltersAsync( ReservationFilter filter, CancellationToken ct = default );
}