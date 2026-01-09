namespace RestaurantBackend.Application.DTOs.Reports;

public sealed class MeseroVentaDto
{
    public int IdMesero { get; init; }

    public string Nombres { get; init; } = string.Empty;

    public string Apellidos { get; init; } = string.Empty;

    public decimal TotalVendido { get; init; }
}