namespace RestaurantBackend.Application.DTOs.Factura;

public sealed class FacturaDetalleDto
{
    public int IdDetalleFactura { get; init; }

    public int IdSupervisor { get; init; }

    public string SupervisorNombreCompleto { get; init; } = string.Empty;

    public string Plato { get; init; } = string.Empty;

    public decimal Valor { get; init; }
}