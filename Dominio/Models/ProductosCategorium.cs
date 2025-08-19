using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class ProductosCategorium
{
    public string IdCategoria { get; set; } = null!;

    public string CodProducto { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public string Distintivo { get; set; } = null!;

    public decimal PrecioVenta { get; set; }

    public int Stock { get; set; }
}
