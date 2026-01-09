using RestaurantBackend.Application.DTOs.Supervisor;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Application.Services.Implementations;

public sealed class SupervisorService(ISupervisorPersistence supervisorPersistence) : ISupervisorService
{
    public Task<IReadOnlyList<SupervisorDto>> ListAsync(CancellationToken cancellationToken = default)
        => supervisorPersistence.ListAsync(cancellationToken);

    public Task<SupervisorDto?> GetByIdAsync(int idSupervisor, CancellationToken cancellationToken = default)
        => supervisorPersistence.GetByIdAsync(idSupervisor, cancellationToken);

    public Task<SupervisorDto> CreateAsync(CreateSupervisorDto request, CancellationToken cancellationToken = default)
        => supervisorPersistence.CreateAsync(request, cancellationToken);

    public Task<bool> UpdateAsync(int idSupervisor, UpdateSupervisorDto request, CancellationToken cancellationToken = default)
        => supervisorPersistence.UpdateAsync(idSupervisor, request, cancellationToken);

    public Task<bool> DeleteAsync(int idSupervisor, CancellationToken cancellationToken = default)
        => supervisorPersistence.DeleteAsync(idSupervisor, cancellationToken);
}