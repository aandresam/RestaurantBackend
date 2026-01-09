namespace RestaurantBackend.Application.DTOs.Supervisor;

public sealed class UpdateSupervisorDto
{
    public string Nombres { get; init; } = string.Empty;

    public string Apellidos { get; init; } = string.Empty;

    public int? Edad { get; init; }

    public int? Antiguedad { get; init; }
}