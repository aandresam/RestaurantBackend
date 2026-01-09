using RestaurantBackend.Application.DTOs.Mesero;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Application.Services.Implementations;

public sealed class MeseroService(IMeseroPersistence meseroPersistence) : IMeseroService
{
    public Task<IReadOnlyList<MeseroDto>> ListAsync(CancellationToken cancellationToken = default)
        => meseroPersistence.ListAsync(cancellationToken);

    public Task<MeseroDto?> GetByIdAsync(int idMesero, CancellationToken cancellationToken = default)
        => meseroPersistence.GetByIdAsync(idMesero, cancellationToken);

    public Task<MeseroDto> CreateAsync(CreateMeseroDto request, CancellationToken cancellationToken = default)
        => meseroPersistence.CreateAsync(request, cancellationToken);

    public Task<bool> UpdateAsync(int idMesero, UpdateMeseroDto request, CancellationToken cancellationToken = default)
        => meseroPersistence.UpdateAsync(idMesero, request, cancellationToken);

    public Task<bool> DeleteAsync(int idMesero, CancellationToken cancellationToken = default)
        => meseroPersistence.DeleteAsync(idMesero, cancellationToken);
}