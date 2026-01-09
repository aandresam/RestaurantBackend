using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Application.DTOs.Mesero;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Persistence.Implementations;

public sealed class MeseroPersistence(RestaurantDbContext dbContext) : IMeseroPersistence
{
    public async Task<IReadOnlyList<MeseroDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Meseros
            .AsNoTracking()
            .OrderBy(x => x.Apellidos)
            .ThenBy(x => x.Nombres)
            .Select(x => new MeseroDto
            {
                IdMesero = x.IdMesero,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Edad = x.Edad,
                Antiguedad = x.Antiguedad,
            })
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<MeseroDto?> GetByIdAsync(int idMesero, CancellationToken cancellationToken = default)
    {
        return await dbContext.Meseros
            .AsNoTracking()
            .Where(x => x.IdMesero == idMesero)
            .Select(x => new MeseroDto
            {
                IdMesero = x.IdMesero,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Edad = x.Edad,
                Antiguedad = x.Antiguedad,
            })
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<MeseroDto> CreateAsync(CreateMeseroDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Mesero
        {
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Edad = request.Edad,
            Antiguedad = request.Antiguedad,
        };

        await dbContext.Meseros.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return new MeseroDto
        {
            IdMesero = entity.IdMesero,
            Nombres = entity.Nombres,
            Apellidos = entity.Apellidos,
            Edad = entity.Edad,
            Antiguedad = entity.Antiguedad,
        };
    }

    public async Task<bool> UpdateAsync(int idMesero, UpdateMeseroDto request, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Meseros
            .FirstOrDefaultAsync(x => x.IdMesero == idMesero, cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return false;
        }

        entity.Nombres = request.Nombres;
        entity.Apellidos = request.Apellidos;
        entity.Edad = request.Edad;
        entity.Antiguedad = request.Antiguedad;

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    public async Task<bool> DeleteAsync(int idMesero, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Meseros
            .FirstOrDefaultAsync(x => x.IdMesero == idMesero, cancellationToken)
            .ConfigureAwait(false);

        if (entity is null)
        {
            return false;
        }

        dbContext.Meseros.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }
}