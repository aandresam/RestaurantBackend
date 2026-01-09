using RestaurantBackend.Application.DTOs.Supervisor;

namespace RestaurantBackend.Application.Persistence;

public interface ISupervisorPersistence
{
    Task<IReadOnlyList<SupervisorDto>> ListAsync(CancellationToken cancellationToken = default);

    Task<SupervisorDto?> GetByIdAsync(int idSupervisor, CancellationToken cancellationToken = default);

    Task<SupervisorDto> CreateAsync(CreateSupervisorDto request, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(int idSupervisor, UpdateSupervisorDto request, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int idSupervisor, CancellationToken cancellationToken = default);
}