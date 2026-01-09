using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Application.Repositories;
using RestaurantBackend.Infrastructure.Persistence;

namespace RestaurantBackend.Infrastructure.Repositories;

public abstract class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
{
    protected EfRepository(RestaurantDbContext dbContext)
    {
        DbContext = dbContext;
        Set = DbContext.Set<TEntity>();
    }

    protected RestaurantDbContext DbContext { get; }

    protected DbSet<TEntity> Set { get; }

    public virtual Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        => Set.FindAsync([id], cancellationToken).AsTask();

    public virtual async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().ToListAsync(cancellationToken);

    public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => Set.AddAsync(entity, cancellationToken).AsTask();

    public virtual void Update(TEntity entity)
        => Set.Update(entity);

    public virtual void Remove(TEntity entity)
        => Set.Remove(entity);
}