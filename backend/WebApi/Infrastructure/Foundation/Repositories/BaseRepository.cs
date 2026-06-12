using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    protected readonly AppDbContext Context;

    public BaseRepository( AppDbContext context )
    {
        Context = context;
    }

    public async Task AddAsync( TEntity entity, CancellationToken ct )
    {
        await Context.Set<TEntity>().AddAsync( entity, ct );
    }

    public void Delete( TEntity entity )
    {
        Context.Set<TEntity>().Remove( entity );
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync( CancellationToken ct )
    {
        return await Context.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync( ct );
    }

    public async Task<TEntity?> GetByIdAsync( int id, CancellationToken ct )
    {
        return await Context.Set<TEntity>().FindAsync( id, ct );
    }
}
