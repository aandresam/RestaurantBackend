namespace RestaurantBackend.Application.DTOs.Mesa;

public sealed class UpdateMesaDto
{
    public string? Nombre { get; init; }

    public bool Reservada { get; init; }

    public int Puestos { get; init; }
}