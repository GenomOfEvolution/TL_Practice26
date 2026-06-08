using Domain.Entities;
using Domain.Filters;

namespace Infrastructure.Foundation.Repositories;

internal static class ReservationFilterExtensions
{
    public static IQueryable<Reservation> ApplyFilter( this IQueryable<Reservation> query, ReservationFilter filter )
    {
        if ( filter.PropertyId.HasValue )
        {
            query = query.Where( r => r.PropertyId == filter.PropertyId.Value );
        }

        if ( !string.IsNullOrWhiteSpace( filter.GuestName ) )
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

        return query;
    }
}
