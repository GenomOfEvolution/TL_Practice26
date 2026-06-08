using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> Entities;

    public BaseRepository( AppDbContext context )
    {
        Entities = context.Set<TEntity>();
    }

    public async Task AddAsync( TEntity entity, CancellationToken ct = default )
    {
        await Entities.AddAsync( entity, ct );
    }

    public void Delete( TEntity entity )
    {
        Entities.Remove( entity );
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync( CancellationToken ct = default )
    {
        return await Entities.ToListAsync( ct );
    }

    public async Task<TEntity?> GetByIdAsync( int id, CancellationToken ct = default )
    {
        return await Entities.FindAsync( id, ct );
    }
}
