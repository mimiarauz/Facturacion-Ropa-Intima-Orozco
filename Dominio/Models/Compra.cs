using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Compra
{
    public int IdCompras { get; set; }

    public DateTime FechaCompras { get; set; }

    public int IdProveedor { get; set; }

    public decimal? TotalGeneral { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}
