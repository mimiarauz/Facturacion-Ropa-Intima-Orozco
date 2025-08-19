using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class DetalleFactura
{
    public int IdDetalleFactura { get; set; }

    public int CantidadProductosF { get; set; }

    public int IdProducto { get; set; }

    public int IdFactura { get; set; }

    public decimal Precio { get; set; }

    public virtual Facturacion IdFacturaNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
