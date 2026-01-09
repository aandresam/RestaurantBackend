namespace RestaurantBackend.Application.DTOs.Factura;

public sealed class ReplaceDetallesFacturaDto
{
    public IReadOnlyList<CreateDetalleFacturaDto> Detalles { get; init; } = [];
}