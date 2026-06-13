using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class RoomTypeRepository : BaseRepository<RoomType>, IRoomTypeRepository
{
    public RoomTypeRepository( AppDbContext context )
        : base( context )
    {
    }

    public async Task<IReadOnlyList<RoomType>> GetByPropertyIdAsync( int propertyId, CancellationToken ct )
    {
        return await Context.Set<RoomType>()
            .Where( rt => rt.PropertyId == propertyId )
            .ToListAsync( ct );
    }
}
