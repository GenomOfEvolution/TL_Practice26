using Application.DTO;

namespace Application.Interfaces;

public interface IPropertyService
{
    Task<int> CreateAsync( CreatePropertyDto dto, CancellationToken ct );
    Task<PropertyDto> GetByIdAsync( int propertyId, CancellationToken ct );
    Task<IReadOnlyList<PropertyDto>> GetAllAsync( CancellationToken ct );
    Task UpdateAsync( UpdatePropertyDto dto, CancellationToken ct );
    Task DeleteAsync( int propertyId, CancellationToken ct );
}
