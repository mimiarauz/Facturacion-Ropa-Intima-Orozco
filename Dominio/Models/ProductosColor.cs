using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class ProductosColor
{
    public string IdColor { get; set; } = null!;

    public string CodProducto { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Distintivo { get; set; } = null!;

    public decimal PrecioVenta { get; set; }

    public int Stock { get; set; }
}
