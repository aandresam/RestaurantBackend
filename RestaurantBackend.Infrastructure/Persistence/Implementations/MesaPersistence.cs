using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Application.DTOs.Mesa;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Persistence.Implementations;

public sealed class MesaPersistence(RestaurantDbContext dbContext) : IMesaPersistence
{
    public async Task<IReadOnlyList<MesaDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Mesas
            .AsNoTracking()
            .OrderBy(x => x.NroMesa)
            .Select(x => new MesaDto
            {
                Id = x.IdMesa,
                NroMesa = x.NroMesa,
                Nombre = x.Nombre,
                Reservada = x.Reservada,
                Puestos = x.Puestos,
            })
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<MesaDto?> GetByNroMesaAsync(int nroMesa, CancellationToken cancellationToken = default)
    {
        return await dbContext.Mesas
            .AsNoTracking()
            .Where(x => x.NroMesa == nroMesa)
            .Select(x => new MesaDto
            {
                Id = x.IdMesa,
                NroMesa = x.NroMesa,
                Nombre = x.Nombre,
                Reservada = x.Reservada,
                Puestos = x.Puestos,
            })
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<MesaDto> CreateAsync(CreateMesaDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Mesa
        {
            NroMesa = request.NroMesa,
            Nombre = request.Nombre,
            Reservada = request.Reservada,
            Puestos = request.Puestos,
        };

        await dbContext.Mesas.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new MesaDto
        {
            NroMesa = entity.NroMesa,
            Nombre = entity.Nombre,
            Reservada = entity.Reservada,
            Puestos = entity.Puestos,
        };
    }

    public async Task<bool> UpdateAsync(int nroMesa, UpdateMesaDto request, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Mesas
            .FirstOrDefaultAsync(x => x.NroMesa == nroMesa, cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return false;
        }

        entity.Nombre = request.Nombre;
        entity.Reservada = request.Reservada;
        entity.Puestos = request.Puestos;

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    public async Task<bool> DeleteAsync(int nroMesa, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Mesas
            .FirstOrDefaultAsync(x => x.NroMesa == nroMesa, cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return false;
        }

        dbContext.Mesas.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }
}