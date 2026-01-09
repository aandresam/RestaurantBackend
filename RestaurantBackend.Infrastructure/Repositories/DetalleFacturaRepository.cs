using RestaurantBackend.Infrastructure.Persistence;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Repositories;

public sealed class DetalleFacturaRepository(RestaurantDbContext dbContext) 
    : EfRepository<DetalleFactura, int>(dbContext)
{
}