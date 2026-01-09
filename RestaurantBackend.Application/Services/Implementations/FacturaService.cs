using RestaurantBackend.Application.DTOs.Factura;
using RestaurantBackend.Application.Persistence;
using RestaurantBackend.Application.Services.Interfaces;

namespace RestaurantBackend.Application.Services.Implementations;

public sealed class FacturaService(IFacturaPersistence facturaPersistence) : IFacturaService
{
    public Task<FacturaDto> CreateFacturaAsync(CreateFacturaDto request, CancellationToken cancellationToken = default)
        => facturaPersistence.CreateFacturaAsync(request, cancellationToken);

    public Task<IReadOnlyList<FacturaFullDto>> ListAsync(DateTime? startDate, DateTime? endDateInclusive, CancellationToken cancellationToken = default)
        => facturaPersistence.ListAsync(startDate, endDateInclusive, cancellationToken);

    public Task<FacturaFullDto?> GetByIdAsync(int idFactura, CancellationToken cancellationToken = default)
        => facturaPersistence.GetByIdAsync(idFactura, cancellationToken);

    public Task<FacturaFullDto?> AddDetalleAsync(int idFactura, AddDetalleFacturaDto request, CancellationToken cancellationToken = default)
        => facturaPersistence.AddDetalleAsync(idFactura, request, cancellationToken);

    public Task<FacturaFullDto?> ReplaceDetallesAsync(int idFactura, ReplaceDetallesFacturaDto request, CancellationToken cancellationToken = default)
        => facturaPersistence.ReplaceDetallesAsync(idFactura, request, cancellationToken);

    public Task<bool> DeleteDetalleAsync(int idFactura, int idDetalleFactura, CancellationToken cancellationToken = default)
        => facturaPersistence.DeleteDetalleAsync(idFactura, idDetalleFactura, cancellationToken);
}