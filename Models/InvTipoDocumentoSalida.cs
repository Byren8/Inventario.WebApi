using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvTipoDocumentoSalida
{
    public decimal TipIdDocumento { get; set; }

    public string? TipDescripcionDocumento { get; set; }

    public virtual ICollection<InvInventarioKardex> InvInventarioKardices { get; set; } = new List<InvInventarioKardex>();
}
