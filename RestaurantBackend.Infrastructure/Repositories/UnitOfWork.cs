using RestaurantBackend.Application.Repositories;
using RestaurantBackend.Infrastructure.Persistence;

namespace RestaurantBackend.Infrastructure.Repositories;

public sealed class UnitOfWork(RestaurantDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);
}