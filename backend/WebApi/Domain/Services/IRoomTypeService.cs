using Domain.Entities;

namespace Domain.Services;

public interface IRoomTypeService
{
    Task<RoomType> CreateAsync( RoomType roomType );
    Task<RoomType?> GetByIdAsync( int id );
    Task<IEnumerable<RoomType>> GetByPropertyIdAsync( int propertyId );
    Task UpdateAsync( RoomType roomType );
    Task DeleteAsync( int id );
}
