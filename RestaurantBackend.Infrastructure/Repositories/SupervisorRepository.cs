using RestaurantBackend.Infrastructure.Persistence;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Repositories;

public sealed class SupervisorRepository(RestaurantDbContext dbContext) 
    : EfRepository<Supervisor, int>(dbContext)
{
}