using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface IReservationRepository : IBaseRepository<Reservation>
{
    Task<IReadOnlyList<Reservation>> GetOverlappingAsync(
        int roomTypeId,
        DateOnly arrival,
        DateOnly departure,
        CancellationToken ct );
    Task<IReadOnlyList<Reservation>> GetByFiltersAsync( ReservationFilter filter, CancellationToken ct );
}