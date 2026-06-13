using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertyRepository : IBaseRepository<Property>
{
    Task<IReadOnlyList<Property>> GetByCityAsync( string city, CancellationToken ct );
}
