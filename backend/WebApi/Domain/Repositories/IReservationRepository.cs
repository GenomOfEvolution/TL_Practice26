using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync( int id );
    Task<Reservation> AddAsync( Reservation reservation );
    Task UpdateAsync( Reservation reservation );
    Task DeleteAsync( int id );

    Task<IEnumerable<Reservation>> GetOverlappingAsync( int roomTypeId, DateOnly arrival, DateOnly departure );
    Task<IEnumerable<Reservation>> GetByFiltersAsync( int? propertyId = null,
                                                    string? guestName = null,
                                                    DateOnly? dateFrom = null,
                                                    DateOnly? dateTo = null );
}