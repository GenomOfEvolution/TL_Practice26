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

    public async Task<IEnumerable<RoomType>> GetByPropertyIdAsync( int propertyId )
    {
        return await Entities
            .Where( rt => rt.PropertyId == propertyId )
            .ToListAsync();
    }
}
