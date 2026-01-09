namespace RestaurantBackend.Application.DTOs.Reports;

public sealed class ProductoMasVendidoDto
{
    public string Plato { get; init; } = string.Empty;

    public int Cantidad { get; init; }

    public decimal TotalFacturado { get; init; }
}