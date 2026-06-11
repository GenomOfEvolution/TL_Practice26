using Application.DTO;

namespace Application.Interfaces;

public interface IRoomTypeService
{
    Task<int> CreateAsync( CreateRoomTypeDto dto, CancellationToken ct = default );
    Task<RoomTypeDto?> GetByIdAsync( int id, CancellationToken ct = default );
    Task<IReadOnlyList<RoomTypeDto>> GetByPropertyIdAsync( int propertyId, CancellationToken ct = default );
    Task UpdateAsync( UpdateRoomTypeDto dto, CancellationToken ct = default );
    Task DeleteAsync( int id, CancellationToken ct = default );
}
