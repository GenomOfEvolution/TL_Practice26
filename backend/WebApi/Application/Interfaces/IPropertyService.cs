using Application.DTO;

namespace Application.Interfaces;

public interface IPropertyService
{
    Task<int> CreateAsync( CreatePropertyDto dto, CancellationToken ct );
    Task<PropertyDto> GetByIdAsync( int id, CancellationToken ct );
    Task<IReadOnlyList<PropertyDto>> GetAllAsync( CancellationToken ct );
    Task UpdateAsync( UpdatePropertyDto dto, CancellationToken ct );
    Task DeleteAsync( int id, CancellationToken ct );
}
