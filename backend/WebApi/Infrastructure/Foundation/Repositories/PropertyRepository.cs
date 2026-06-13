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

    public async Task<IReadOnlyList<Property>> GetByCityAsync( string city, CancellationToken ct )
    {
        return await Context.Set<Property>()
            .Where( p => p.City == city )
            .ToListAsync( ct );
    }
}
