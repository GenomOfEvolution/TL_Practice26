using Domain.Entities;

namespace Domain.Services;

public interface IPropertyService
{
    Task<Property> CreateAsync( Property property, CancellationToken ct = default );
    Task<Property?> GetByIdAsync( int id, CancellationToken ct = default );
    Task<IEnumerable<Property>> GetAllAsync( CancellationToken ct = default );
    Task UpdateAsync( Property property, CancellationToken ct = default );
    Task DeleteAsync( int id, CancellationToken ct = default );
}