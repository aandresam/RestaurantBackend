using RestaurantBackend.Application.DTOs.Factura;

namespace RestaurantBackend.Application.Persistence;

public interface IFacturaPersistence
{
    Task<FacturaDto> CreateFacturaAsync(CreateFacturaDto request, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FacturaFullDto>> ListAsync(DateTime? desde, DateTime? hastaInclusive, CancellationToken cancellationToken = default);

    Task<FacturaFullDto?> GetByIdAsync(int idFactura, CancellationToken cancellationToken = default);

    Task<FacturaFullDto?> AddDetalleAsync(int idFactura, AddDetalleFacturaDto request, CancellationToken cancellationToken = default);

    Task<FacturaFullDto?> ReplaceDetallesAsync(int idFactura, ReplaceDetallesFacturaDto request, CancellationToken cancellationToken = default);

    Task<bool> DeleteDetalleAsync(int idFactura, int idDetalleFactura, CancellationToken cancellationToken = default);
}