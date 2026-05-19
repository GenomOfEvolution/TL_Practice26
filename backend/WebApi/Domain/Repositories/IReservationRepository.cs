using Domain.Entities;
using Domain.Filters;

namespace Domain.Repositories;

public interface IReservationRepository : IBaseRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetOverlappingAsync( int roomTypeId, DateOnly arrival, DateOnly departure );
    Task<IEnumerable<Reservation>> GetByFiltersAsync( ReservationFilter filter );
}