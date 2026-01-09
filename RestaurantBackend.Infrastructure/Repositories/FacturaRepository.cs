using RestaurantBackend.Infrastructure.Persistence;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Repositories;

public sealed class FacturaRepository(RestaurantDbContext dbContext) 
    : EfRepository<Factura, int>(dbContext)
{
}