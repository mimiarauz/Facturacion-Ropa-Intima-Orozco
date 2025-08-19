using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class TotalFacturaVenta
{
    public int IdFactura { get; set; }

    public decimal? TotalFactura { get; set; }
}
