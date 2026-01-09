namespace RestaurantBackend.Application.DTOs.Mesero;

public sealed class MeseroDto
{
    public int IdMesero { get; init; }

    public string Nombres { get; init; } = string.Empty;

    public string Apellidos { get; init; } = string.Empty;

    public int? Edad { get; init; }

    public int? Antiguedad { get; init; }
}