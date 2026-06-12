namespace Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IReadOnlyList<TEntity>> GetAllAsync( CancellationToken ct );
    Task<TEntity?> GetByIdAsync( int id, CancellationToken ct );
    Task AddAsync( TEntity entity, CancellationToken ct );
    void Delete( TEntity entity );
}
