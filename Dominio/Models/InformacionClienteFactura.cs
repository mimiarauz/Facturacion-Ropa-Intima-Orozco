using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class InformacionClienteFactura
{
    public int Id { get; set; }
    public string Cedula { get; set; } = null!;

    public string NombrePersona { get; set; } = null!;

    public string ApellidoPersona { get; set; } = null!;

    public string DireccionCliente { get; set; } = null!;

    public int NumCliente { get; set; }

    public int IdFactura { get; set; }

    public DateTime FechaFactura { get; set; }

    public decimal DescuentoFactura { get; set; }

    public decimal TotaFactura { get; set; }
}
