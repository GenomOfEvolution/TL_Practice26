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

    public async Task AddAsync( TEntity entity )
    {
        await Entities.AddAsync( entity );
    }

    public void Delete( TEntity entity )
    {
        Entities.Remove( entity );
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Entities.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync( int id )
    {
        return await Entities.FindAsync( id );
    }

    public void Update( TEntity entity )
    {
        Entities.Update( entity );
    }
}
