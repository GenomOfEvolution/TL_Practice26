using Domain.Entities;
using Domain.Filters;

namespace Domain.Services;

public interface IReservationService
{
    Task<Reservation> CreateAsync( Reservation reservation );
    Task<Reservation?> GetByIdAsync( int id );
    Task<IEnumerable<Reservation>> GetListAsync( ReservationFilter filter );
    Task CancelAsync( int id );
}
