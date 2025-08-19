using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class InformacionUsuarioFactura
{
    public int Id { get; set; }
    public string Cedula { get; set; } = null!;

    public string NombrePersona { get; set; } = null!;

    public string ApellidoPersona { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string TipoUsuario { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public int IdFactura { get; set; }

    public DateTime FechaFactura { get; set; }

    public int IdProducto { get; set; }

    public int CantidadProductosF { get; set; }

    public decimal Precio { get; set; }

    public decimal DescuentoFactura { get; set; }

    public decimal TotaFactura { get; set; }
}
