using Application.DTO;

namespace Application.Interfaces;

public interface IPropertyService
{
    Task<int> CreateAsync( CreatePropertyDto dto, CancellationToken ct = default );
    Task<PropertyDto?> GetByIdAsync( int id, CancellationToken ct = default );
    Task<IReadOnlyList<PropertyDto>> GetAllAsync( CancellationToken ct = default );
    Task UpdateAsync( UpdatePropertyDto dto, CancellationToken ct = default );
    Task DeleteAsync( int id, CancellationToken ct = default );
}
