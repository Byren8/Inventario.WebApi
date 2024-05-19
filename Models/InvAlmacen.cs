using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvAlmacen
{
    public decimal AlmIdAlmacen { get; set; }

    public string? AlmNombreAlmacen { get; set; }

    public string? AlmDireccion { get; set; }

    public virtual ICollection<InvInventarioKardex> InvInventarioKardices { get; set; } = new List<InvInventarioKardex>();

    public virtual ICollection<InvTrasladoAlmacen> InvTrasladoAlmacens { get; set; } = new List<InvTrasladoAlmacen>();
}
