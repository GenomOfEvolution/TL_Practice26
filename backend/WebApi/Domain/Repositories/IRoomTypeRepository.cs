using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeRepository
{
    Task<IEnumerable<RoomType>> GetAllAsync();
    Task<RoomType?> GetByIdAsync( int id );
    Task<RoomType> AddAsync( RoomType roomType );
    Task UpdateAsync( RoomType roomType );
    Task DeleteAsync( int id );

    Task<IEnumerable<RoomType>> GetByPropertyIdAsync( int propertyId );
}
