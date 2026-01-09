using RestaurantBackend.Infrastructure.Persistence;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Repositories;

public sealed class MeseroRepository(RestaurantDbContext dbContext) 
    : EfRepository<Mesero, int>(dbContext)
{
}