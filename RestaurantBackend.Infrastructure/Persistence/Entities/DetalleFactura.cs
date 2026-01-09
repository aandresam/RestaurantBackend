using System;
using System.Collections.Generic;

namespace RestaurantBackend.Infrastructure.Persistence.Entities;

public partial class DetalleFactura
{
    public int IdDetalleFactura { get; set; }

    public int IdFactura { get; set; }

    public int IdSupervisor { get; set; }

    public string Plato { get; set; } = null!;

    public decimal Valor { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    public virtual Supervisor IdSupervisorNavigation { get; set; } = null!;
}
