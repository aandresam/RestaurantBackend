using RestaurantBackend.Infrastructure.Persistence.Entities;
using RestaurantBackend.Infrastructure.Persistence;

namespace RestaurantBackend.Infrastructure.Repositories;

public sealed class ClienteRepository(RestaurantDbContext dbContext) 
    : EfRepository<Cliente, int>(dbContext)
{
}