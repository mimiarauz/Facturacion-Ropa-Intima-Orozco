using System;
using System.Collections.Generic;

namespace Proyecto.Models;

public partial class Marca
{
    public string IdMarca { get; set; } = null!;

    public string Marca1 { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
