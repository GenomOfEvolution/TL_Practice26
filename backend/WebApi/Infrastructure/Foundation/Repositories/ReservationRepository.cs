using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
{
    public ReservationRepository( AppDbContext context )
        : base( context )
    {
    }

    public async Task<IEnumerable<Reservation>> GetByFiltersAsync( ReservationFilter filter, CancellationToken ct = default )
    {
        return await Context.Set<Reservation>()
            .ApplyFilter( filter )
            .ToListAsync( ct );
    }

    public async Task<IEnumerable<Reservation>> GetOverlappingAsync( int roomTypeId, DateOnly arrival, DateOnly departure, CancellationToken ct = default )
    {
        return await Context.Set<Reservation>()
            .Where( r => r.RoomTypeId == roomTypeId
                && !r.IsCanceled
                && r.ArrivalDate < departure
                && r.DepartureDate > arrival )
            .ToListAsync( ct );
    }
}
