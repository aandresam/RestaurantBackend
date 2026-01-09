namespace RestaurantBackend.Application.DTOs.Factura;

public sealed class CreateFacturaDto
{
    public int IdCliente { get; init; }

    public int IdMesa { get; init; }

    public int IdMesero { get; init; }

    public IReadOnlyList<CreateDetalleFacturaDto> Detalles { get; init; } = [];
}