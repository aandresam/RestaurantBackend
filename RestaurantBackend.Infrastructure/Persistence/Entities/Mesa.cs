using System;
using System.Collections.Generic;

namespace RestaurantBackend.Infrastructure.Persistence.Entities;

public partial class Mesa
{
    public int IdMesa { get; set; }

    public int NroMesa { get; set; }

    public string? Nombre { get; set; }

    public bool Reservada { get; set; }

    public int Puestos { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
