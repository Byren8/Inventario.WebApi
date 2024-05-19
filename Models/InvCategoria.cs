using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvCategoria
{
    public decimal CatIdCategoria { get; set; }

    public string? CatNombreCategoria { get; set; }

    public string? CatDescCategoria { get; set; }

    public virtual ICollection<InvUnidadDeMedidum> InvUnidadDeMedida { get; set; } = new List<InvUnidadDeMedidum>();
}
