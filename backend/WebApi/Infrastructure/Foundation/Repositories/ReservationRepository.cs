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

    public async Task<IEnumerable<Reservation>> GetByFiltersAsync( ReservationFilter filter )
    {
        IQueryable<Reservation> query = Entities;

        if ( filter.PropertyId.HasValue )
        {
            query = query.Where( r => r.PropertyId == filter.PropertyId.Value );
        }

        if ( !String.IsNullOrWhiteSpace( filter.GuestName ) )
        {
            query = query.Where( r => r.GuestName.Contains( filter.GuestName ) );
        }

        if ( filter.DateFrom.HasValue )
        {
            query = query.Where( r => r.ArrivalDate >= filter.DateFrom.Value );
        }

        if ( filter.DateTo.HasValue )
        {
            query = query.Where( r => r.DepartureDate <= filter.DateTo.Value );
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetOverlappingAsync( int roomTypeId, DateOnly arrival, DateOnly departure )
    {
        return await Entities
            .Where( r => r.RoomTypeId == roomTypeId
                     && r.ArrivalDate < departure
                     && r.DepartureDate > arrival )
            .ToListAsync();
    }
}
