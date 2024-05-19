using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvProducto
{
    public decimal ProIdProducto { get; set; }

    public decimal? PrvIdProveedor { get; set; }

    public string? ProNombreProducto { get; set; }

    public decimal? ProPrecioDeCompra { get; set; }

    public decimal? ProPrecioVenta { get; set; }

    public string? ProStockMinimo { get; set; }

    public string? ProStockMaximo { get; set; }

    public byte[]? ProImagenDeProducto { get; set; }

    public virtual ICollection<InvInventarioKardex> InvInventarioKardices { get; set; } = new List<InvInventarioKardex>();
}
