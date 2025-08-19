using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string TipoUsuario { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Facturacion> Facturacions { get; set; } = new List<Facturacion>();

    public virtual Persona IdUsuarioNavigation { get; set; } = null!;
}
