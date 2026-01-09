namespace RestaurantBackend.Application.DTOs.Factura;

public sealed class CreateDetalleFacturaDto
{
    public int IdSupervisor { get; init; }

    public string Plato { get; init; } = string.Empty;

    public decimal Valor { get; init; }
}