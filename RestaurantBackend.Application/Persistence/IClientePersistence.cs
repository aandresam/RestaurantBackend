using RestaurantBackend.Application.DTOs.Cliente;

namespace RestaurantBackend.Application.Persistence;

public interface IClientePersistence
{
    Task<IReadOnlyList<ClienteDto>> ListAsync(CancellationToken cancellationToken = default);

    Task<ClienteDto?> GetByIdAsync(int idCliente, CancellationToken cancellationToken = default);

    Task<ClienteDto> CreateAsync(CreateClienteDto request, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(int idCliente, UpdateClienteDto request, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int idCliente, CancellationToken cancellationToken = default);
}