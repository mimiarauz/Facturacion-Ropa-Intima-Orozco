using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class ProductosMarca
{
    public string IdMarca { get; set; } = null!;

    public string CodProducto { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string Distintivo { get; set; } = null!;

    public decimal PrecioVenta { get; set; }

    public int Stock { get; set; }
}
