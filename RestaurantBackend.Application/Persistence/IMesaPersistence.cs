using RestaurantBackend.Application.DTOs.Mesa;

namespace RestaurantBackend.Application.Persistence;

public interface IMesaPersistence
{
    Task<IReadOnlyList<MesaDto>> ListAsync(CancellationToken cancellationToken = default);

    Task<MesaDto?> GetByNroMesaAsync(int nroMesa, CancellationToken cancellationToken = default);

    Task<MesaDto> CreateAsync(CreateMesaDto request, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(int nroMesa, UpdateMesaDto request, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int nroMesa, CancellationToken cancellationToken = default);
}