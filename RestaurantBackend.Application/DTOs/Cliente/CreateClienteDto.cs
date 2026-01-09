namespace RestaurantBackend.Application.DTOs.Cliente;

public sealed class CreateClienteDto
{
    public string Identificacion { get; init; } = string.Empty;

    public string Nombres { get; init; } = string.Empty;

    public string Apellidos { get; init; } = string.Empty;

    public string? Direccion { get; init; }

    public string? Telefono { get; init; }
}