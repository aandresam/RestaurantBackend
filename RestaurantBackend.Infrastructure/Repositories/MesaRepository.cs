using RestaurantBackend.Infrastructure.Persistence;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Repositories;

public sealed class MesaRepository(RestaurantDbContext dbContext) 
    : EfRepository<Mesa, int>(dbContext)
{
}