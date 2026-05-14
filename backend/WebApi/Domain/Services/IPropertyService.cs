using Domain.Entities;

namespace Domain.Services;

public interface IPropertyService
{
    Task<Property> CreateAsync( Property property );
    Task<Property?> GetByIdAsync( int id );
    Task<IEnumerable<Property>> GetAllAsync();
    Task UpdateAsync( Property property );
    Task DeleteAsync( int id );
}