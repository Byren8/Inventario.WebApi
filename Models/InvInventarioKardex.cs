using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvInventarioKardex
{
    public decimal IneIdInventario { get; set; }

    public decimal? InvDetalleRecepcion { get; set; }

    public decimal? ProIdProducto { get; set; }

    public decimal? AlmIdAlmacen { get; set; }

    public decimal? InvTipoDocumentoSalida { get; set; }

    public decimal? PrvIdProveedor { get; set; }

    public decimal? IneCantidadStock { get; set; }

    public DateTime? IneFechaIngreso { get; set; }

    public DateTime? IneFechaSalida { get; set; }

    public virtual InvAlmacen? AlmIdAlmacenNavigation { get; set; }

    public virtual InvDetalleRecepcion? InvDetalleRecepcionNavigation { get; set; }

    public virtual InvTipoDocumentoSalida? InvTipoDocumentoSalidaNavigation { get; set; }

    public virtual InvProducto? ProIdProductoNavigation { get; set; }

    public virtual InvProveedore? PrvIdProveedorNavigation { get; set; }
}
