using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Facturacion
{
    public int IdFactura { get; set; }

    public DateTime FechaFactura { get; set; }

    public decimal DescuentoFactura { get; set; }

    public int IdCliente { get; set; }

    public int IdUsuario { get; set; }

    public decimal TotaFactura { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
