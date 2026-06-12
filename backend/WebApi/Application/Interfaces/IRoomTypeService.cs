using Application.DTO;

namespace Application.Interfaces;

public interface IRoomTypeService
{
    Task<int> CreateAsync( CreateRoomTypeDto dto, CancellationToken ct );
    Task<RoomTypeDto> GetByIdAsync( int id, CancellationToken ct );
    Task<IReadOnlyList<RoomTypeDto>> GetByPropertyIdAsync( int propertyId, CancellationToken ct );
    Task UpdateAsync( UpdateRoomTypeDto dto, CancellationToken ct );
    Task DeleteAsync( int id, CancellationToken ct );
}
