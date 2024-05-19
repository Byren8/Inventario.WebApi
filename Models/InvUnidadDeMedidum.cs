using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvUnidadDeMedidum
{
    public decimal UndIdUnidadDeMedida { get; set; }

    public decimal? CatIdCategoria { get; set; }

    public string? UndNombre { get; set; }

    public string? UndAbreviatura { get; set; }

    public virtual InvCategoria? CatIdCategoriaNavigation { get; set; }

    public virtual ICollection<InvProveedore> InvProveedores { get; set; } = new List<InvProveedore>();
}
