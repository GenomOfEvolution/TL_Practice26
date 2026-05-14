using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync( int id );
    Task<Property> AddAsync( Property property );
    Task UpdateAsync( Property property );
    Task DeleteAsync( int id );

    Task<IEnumerable<Property>> GetByCityAsync( string city );
}
