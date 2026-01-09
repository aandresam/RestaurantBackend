using System;
using System.Collections.Generic;

namespace RestaurantBackend.Infrastructure.Persistence.Entities;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Identificacion { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
