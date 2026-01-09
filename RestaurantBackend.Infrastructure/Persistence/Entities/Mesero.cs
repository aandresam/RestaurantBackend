using System;
using System.Collections.Generic;

namespace RestaurantBackend.Infrastructure.Persistence.Entities;

public partial class Mesero
{
    public int IdMesero { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public int? Edad { get; set; }

    public int? Antiguedad { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
