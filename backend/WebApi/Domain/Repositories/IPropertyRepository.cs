using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertyRepository : IBaseRepository<Property>
{
    Task<IEnumerable<Property>> GetByCityAsync( string city );
}
