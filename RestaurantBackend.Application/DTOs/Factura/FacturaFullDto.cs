namespace RestaurantBackend.Application.DTOs.Factura;

public sealed class FacturaFullDto
{
    public int IdFactura { get; init; }

    public int NroFactura { get; init; }

    public DateTime Fecha { get; init; }

    public int IdCliente { get; init; }

    public string ClienteNombreCompleto { get; init; } = string.Empty;

    public int IdMesa { get; init; }

    public int IdMesero { get; init; }

    public string MeseroNombreCompleto { get; init; } = string.Empty;

    public decimal TotalFacturado { get; init; }

    public IReadOnlyList<FacturaDetalleDto> Detalles { get; init; } = [];
}