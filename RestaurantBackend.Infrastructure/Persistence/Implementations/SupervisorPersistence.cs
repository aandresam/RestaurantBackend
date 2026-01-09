using Microsoft.EntityFrameworkCore;
using RestaurantBackend.Application.DTOs.Supervisor;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Infrastructure.Persistence.Entities;

namespace RestaurantBackend.Infrastructure.Persistence.Implementations;

public sealed class SupervisorPersistence(RestaurantDbContext dbContext) : ISupervisorPersistence
{
    public async Task<IReadOnlyList<SupervisorDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Supervisors
            .AsNoTracking()
            .OrderBy(x => x.Apellidos)
            .ThenBy(x => x.Nombres)
            .Select(x => new SupervisorDto
            {
                IdSupervisor = x.IdSupervisor,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Edad = x.Edad,
                Antiguedad = x.Antiguedad,
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<SupervisorDto?> GetByIdAsync(int idSupervisor, CancellationToken cancellationToken = default)
    {
        return await dbContext.Supervisors
            .AsNoTracking()
            .Where(x => x.IdSupervisor == idSupervisor)
            .Select(x => new SupervisorDto
            {
                IdSupervisor = x.IdSupervisor,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Edad = x.Edad,
                Antiguedad = x.Antiguedad,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SupervisorDto> CreateAsync(CreateSupervisorDto request, CancellationToken cancellationToken = default)
    {
        var entity = new Supervisor
        {
            Nombres = request.Nombres,
            Apellidos = request.Apellidos,
            Edad = request.Edad,
            Antiguedad = request.Antiguedad,
        };

        await dbContext.Supervisors.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new SupervisorDto
        {
            IdSupervisor = entity.IdSupervisor,
            Nombres = entity.Nombres,
            Apellidos = entity.Apellidos,
            Edad = entity.Edad,
            Antiguedad = entity.Antiguedad,
        };
    }

    public async Task<bool> UpdateAsync(int idSupervisor, UpdateSupervisorDto request, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Supervisors
            .FirstOrDefaultAsync(x => x.IdSupervisor == idSupervisor, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        entity.Nombres = request.Nombres;
        entity.Apellidos = request.Apellidos;
        entity.Edad = request.Edad;
        entity.Antiguedad = request.Antiguedad;

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(int idSupervisor, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Supervisors
            .FirstOrDefaultAsync(x => x.IdSupervisor == idSupervisor, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        dbContext.Supervisors.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}