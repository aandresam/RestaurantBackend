using RestaurantBackend.Application.DTOs.Cliente;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Application.Services.Implementations;

public sealed class ClienteService(IClientePersistence clientePersistence) : IClienteService
{
    public Task<IReadOnlyList<ClienteDto>> ListAsync(CancellationToken cancellationToken = default)
        => clientePersistence.ListAsync(cancellationToken);

    public Task<ClienteDto?> GetByIdAsync(int idCliente, CancellationToken cancellationToken = default)
        => clientePersistence.GetByIdAsync(idCliente, cancellationToken);

    public Task<ClienteDto> CreateAsync(CreateClienteDto request, CancellationToken cancellationToken = default)
        => clientePersistence.CreateAsync(request, cancellationToken);

    public Task<bool> UpdateAsync(int idCliente, UpdateClienteDto request, CancellationToken cancellationToken = default)
        => clientePersistence.UpdateAsync(idCliente, request, cancellationToken);

    public Task<bool> DeleteAsync(int idCliente, CancellationToken cancellationToken = default)
        => clientePersistence.DeleteAsync(idCliente, cancellationToken);
}