namespace Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync( CancellationToken ct = default );
    Task<TEntity?> GetByIdAsync( int id, CancellationToken ct = default );
    Task AddAsync( TEntity entity, CancellationToken ct = default );
    void Delete( TEntity entity );
}
