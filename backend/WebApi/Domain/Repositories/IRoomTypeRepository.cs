using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeRepository : IBaseRepository<RoomType>
{
    Task<IEnumerable<RoomType>> GetByPropertyIdAsync( int propertyId, CancellationToken ct = default );
}
