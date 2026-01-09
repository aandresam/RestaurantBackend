using RestaurantBackend.Application.DTOs.Mesa;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Application.Services.Implementations;

public sealed class MesaService(IMesaPersistence mesaPersistence) : IMesaService
{
    public Task<IReadOnlyList<MesaDto>> ListAsync(CancellationToken cancellationToken = default)
        => mesaPersistence.ListAsync(cancellationToken);

    public Task<MesaDto?> GetByNroMesaAsync(int nroMesa, CancellationToken cancellationToken = default)
        => mesaPersistence.GetByNroMesaAsync(nroMesa, cancellationToken);

    public Task<MesaDto> CreateAsync(CreateMesaDto request, CancellationToken cancellationToken = default)
        => mesaPersistence.CreateAsync(request, cancellationToken);

    public Task<bool> UpdateAsync(int nroMesa, UpdateMesaDto request, CancellationToken cancellationToken = default)
        => mesaPersistence.UpdateAsync(nroMesa, request, cancellationToken);

    public Task<bool> DeleteAsync(int nroMesa, CancellationToken cancellationToken = default)
        => mesaPersistence.DeleteAsync(nroMesa, cancellationToken);
}