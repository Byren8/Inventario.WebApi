using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvDetalleRecepcion
{
    public decimal DetIdDetalle { get; set; }

    public decimal? RecIdRecepcion { get; set; }

    public decimal? DetCantidadRecibida { get; set; }

    public decimal? DetPrecioUnitarioCompra { get; set; }

    public decimal? DetSubtotal { get; set; }

    public virtual ICollection<InvInventarioKardex> InvInventarioKardices { get; set; } = new List<InvInventarioKardex>();

    public virtual InvRecepcion? RecIdRecepcionNavigation { get; set; }
}
