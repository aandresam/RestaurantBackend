namespace RestaurantBackend.Application.DTOs.Supervisor;

public sealed class SupervisorDto
{
    public int IdSupervisor { get; init; }

    public string Nombres { get; init; } = string.Empty;

    public string Apellidos { get; init; } = string.Empty;

    public int? Edad { get; init; }

    public int? Antiguedad { get; init; }
}