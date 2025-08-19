using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class DetalleCompra
{
    public int IdDetalleCompras { get; set; }

    public int IdCompras { get; set; }

    public int CantidadProductosC { get; set; }

    public int IdProducto { get; set; }

    public decimal PrecioC { get; set; }

    public decimal PrecioV { get; set; }

    public virtual Compra IdComprasNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
