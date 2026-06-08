using Domain.Entities;

namespace Domain.Services;

public interface IPropertyService
{
    Task<int> CreateAsync( Property property, CancellationToken ct = default );
    Task<Property?> GetByIdAsync( int id, CancellationToken ct = default );
    Task<IReadOnlyList<Property>> GetAllAsync( CancellationToken ct = default );
    Task UpdateAsync( Property property, CancellationToken ct = default );
    Task DeleteAsync( int id, CancellationToken ct = default );
}