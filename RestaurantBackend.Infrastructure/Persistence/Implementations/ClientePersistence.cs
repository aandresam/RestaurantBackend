using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Application.DTOs.Cliente;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Persistence.Implementations;

public sealed class ClientePersistence(RestaurantDbContext dbContext) : IClientePersistence
{
    public async Task<IReadOnlyList<ClienteDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Clientes
            .AsNoTracking()
            .OrderBy(x => x.Apellidos)
            .ThenBy(x => x.Nombres)
            .Select(x => new ClienteDto
            {
                IdCliente = x.IdCliente,
                Identificacion = x.Identificacion,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Direccion = x.Direccion,
                Telefono = x.Telefono,
            })
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ClienteDto?> GetByIdAsync(int idCliente, CancellationToken cancellationToken = default)
    {
        return await dbContext.Clientes
            .AsNoTracking()
            .Where(x => x.IdCliente == idCliente)
            .Select(x => new ClienteDto
            {
                IdCliente = x.IdCliente,
                Identificacion = x.Identificacion,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Direccion = x.Direccion,
                Telefono = x.Telefono,
            })
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ClienteDto> CreateAsync(CreateClienteDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Cliente
        {
            Identificacion = request.Identificacion,
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Direccion = request.Direccion,
            Telefono = request.Telefono,
        };

        await dbContext.Clientes.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new ClienteDto
        {
            IdCliente = entity.IdCliente,
            Identificacion = entity.Identificacion,
            Nombres = entity.Nombres,
            Apellidos = entity.Apellidos,
            Direccion = entity.Direccion,
            Telefono = entity.Telefono,
        };
    }

    public async Task<bool> UpdateAsync(int idCliente, UpdateClienteDto request, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Clientes
            .FirstOrDefaultAsync(x => x.IdCliente == idCliente, cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return false;
        }

        entity.Identificacion = request.Identificacion;
        entity.Nombres = request.Nombres;
        entity.Apellidos = request.Apellidos;
        entity.Direccion = request.Direccion;
        entity.Telefono = request.Telefono;

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    public async Task<bool> DeleteAsync(int idCliente, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Clientes
            .FirstOrDefaultAsync(x => x.IdCliente == idCliente, cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return false;
        }

        dbContext.Clientes.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }
}