namespace RestaurantBackend.Application.DTOs.Factura;

public sealed class AddDetalleFacturaDto
{
    public int IdSupervisor { get; init; }

    public string Plato { get; init; } = string.Empty;

    public decimal Valor { get; init; }
}