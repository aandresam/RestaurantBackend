namespace RestaurantBackend.Application.DTOs.Reports;

public sealed class ClienteConsumoDto
{
    public int IdCliente { get; init; }

    public string Identificacion { get; init; } = string.Empty;

    public string Nombres { get; init; } = string.Empty;

    public string Apellidos { get; init; } = string.Empty;

    public decimal TotalConsumo { get; init; }
}