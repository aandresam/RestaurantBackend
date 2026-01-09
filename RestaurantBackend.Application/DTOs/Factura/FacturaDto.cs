namespace RestaurantBackend.Application.DTOs.Factura;

public sealed class FacturaDto
{
    public int IdFactura { get; init; }

    public int NroFactura { get; init; }

    public DateTime Fecha { get; init; }

    public decimal TotalFacturado { get; init; }
}