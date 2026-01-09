using RestaurantBackend.Application.DTOs.Mesero;

namespace RestaurantBackend.Application.Persistence;

public interface IMeseroPersistence
{
    Task<IReadOnlyList<MeseroDto>> ListAsync(CancellationToken cancellationToken = default);

    Task<MeseroDto?> GetByIdAsync(int idMesero, CancellationToken cancellationToken = default);

    Task<MeseroDto> CreateAsync(CreateMeseroDto request, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(int idMesero, UpdateMeseroDto request, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int idMesero, CancellationToken cancellationToken = default);
}