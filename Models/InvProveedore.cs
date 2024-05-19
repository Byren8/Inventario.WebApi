using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvProveedore
{
    public decimal PrvIdProveedor { get; set; }

    public decimal? UndIdUnidadDeMedida { get; set; }

    public string? PrvNombreProveedor { get; set; }

    public string? PrvDireccionProveedor { get; set; }

    public decimal? PrvNitProveedor { get; set; }

    public virtual ICollection<InvInventarioKardex> InvInventarioKardices { get; set; } = new List<InvInventarioKardex>();

    public virtual ICollection<InvRecepcion> InvRecepcions { get; set; } = new List<InvRecepcion>();

    public virtual InvUnidadDeMedidum? UndIdUnidadDeMedidaNavigation { get; set; }
}
