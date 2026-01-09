using System;
using System.Collections.Generic;

namespace RestaurantBackend.Infrastructure.Persistence.Entities;

public partial class Factura
{
    public int IdFactura { get; set; }

    public int NroFactura { get; set; }

    public int IdCliente { get; set; }

    public int IdMesa { get; set; }

    public int IdMesero { get; set; }

    public DateTime Fecha { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Mesa IdMesaNavigation { get; set; } = null!;

    public virtual Mesero IdMeseroNavigation { get; set; } = null!;
}
