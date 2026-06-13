using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeRepository : IBaseRepository<RoomType>
{
    Task<IReadOnlyList<RoomType>> GetByPropertyIdAsync( int propertyId, CancellationToken ct );
}
