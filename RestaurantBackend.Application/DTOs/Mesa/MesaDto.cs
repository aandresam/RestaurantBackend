namespace RestaurantBackend.Application.DTOs.Mesa;

public sealed class MesaDto
{
    public int Id { get; set; }
    public int NroMesa { get; init; }

    public string? Nombre { get; init; }

    public bool Reservada { get; init; }

    public int Puestos { get; init; }
}