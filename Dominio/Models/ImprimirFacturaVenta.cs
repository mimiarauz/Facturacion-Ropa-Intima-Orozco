using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class ImprimirFacturaVenta
{
    public int Id { get; set; }

    public string NombrePersona { get; set; } = null!;

    public string ApellidoPersona { get; set; } = null!;

    public string DireccionCliente { get; set; } = null!;

    public int NumCliente { get; set; }

    public int IdDetalleFactura { get; set; }

    public int IdFactura { get; set; }

    public DateTime FechaFactura { get; set; }

    public int IdProducto { get; set; }

    public string CodProducto { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public string Distintivo { get; set; } = null!;

    public int CantidadProductosF { get; set; }

    public decimal PrecioVenta { get; set; }

    public decimal TotaFactura { get; set; }
}
