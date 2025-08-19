using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class ProductosTalla
{
    public string IdTalla { get; set; } = null!;

    public string CodProducto { get; set; } = null!;

    public string Talla { get; set; } = null!;

    public string Distintivo { get; set; } = null!;

    public decimal PrecioVenta { get; set; }

    public int Stock { get; set; }
}
