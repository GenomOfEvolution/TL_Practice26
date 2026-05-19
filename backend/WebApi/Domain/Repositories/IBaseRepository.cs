using Domain.Entities;

namespace Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync( int id );
    Task AddAsync( TEntity entity );
    void Update( TEntity entity );
    void Delete( TEntity entity );
}
