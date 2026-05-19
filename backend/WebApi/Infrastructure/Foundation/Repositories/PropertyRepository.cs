using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
{
    public PropertyRepository( AppDbContext context )
        : base( context )
    {
    }

    public async Task<IEnumerable<Property>> GetByCityAsync( string city )
    {
        return await Entities
            .Where( p => p.City == city )
            .ToListAsync();
    }
}
