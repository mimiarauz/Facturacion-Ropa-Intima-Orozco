using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class StockProximoAgotarse
{
    public string CodProducto { get; set; } = null!;

    public string Distintivo { get; set; } = null!;

    public int Stock { get; set; }
}
