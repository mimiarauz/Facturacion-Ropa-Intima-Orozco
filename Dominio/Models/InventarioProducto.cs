using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class InventarioProducto
{
    public string CodProducto { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Talla { get; set; } = null!;

    public string Distintivo { get; set; } = null!;

    public decimal PrecioVenta { get; set; }

    public int Stock { get; set; }
}
