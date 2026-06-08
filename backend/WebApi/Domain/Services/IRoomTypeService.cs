using Domain.Entities;

namespace Domain.Services;

public interface IRoomTypeService
{
    Task<int> CreateAsync( RoomType roomType, CancellationToken ct = default );
    Task<RoomType?> GetByIdAsync( int id, CancellationToken ct = default );
    Task<IReadOnlyList<RoomType>> GetByPropertyIdAsync( int propertyId, CancellationToken ct = default );
    Task UpdateAsync( RoomType roomType, CancellationToken ct = default );
    Task DeleteAsync( int id, CancellationToken ct = default );
}
