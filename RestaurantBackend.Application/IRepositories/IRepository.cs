namespace RestaurantBackend.Application.Repositories;

public interface IRepository<TEntity, in TKey>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}