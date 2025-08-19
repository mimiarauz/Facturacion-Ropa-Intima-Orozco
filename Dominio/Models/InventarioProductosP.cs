using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class InventarioProductosP
{
    public string CodProducto { get; set; } = null!;

    public string Distintivo { get; set; } = null!;

    public decimal PrecioVenta { get; set; }

    public int Stock { get; set; }

    public int? ComprasTotales { get; set; }

    public int? VentasTotales { get; set; }

    public int? StockActual { get; set; }
}
