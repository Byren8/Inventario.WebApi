using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvRecepcion
{
    public decimal RecIdRecepcion { get; set; }

    public decimal? PrvIdProveedor { get; set; }

    public DateTime? RecFechaRecepcion { get; set; }

    public decimal? RecTotalFactura { get; set; }

    public virtual ICollection<InvDetalleRecepcion> InvDetalleRecepcions { get; set; } = new List<InvDetalleRecepcion>();

    public virtual InvProveedore? PrvIdProveedorNavigation { get; set; }
}
