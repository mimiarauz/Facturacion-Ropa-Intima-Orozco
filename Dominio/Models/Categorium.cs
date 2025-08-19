using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Categorium
{
    public string IdCategoria { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
