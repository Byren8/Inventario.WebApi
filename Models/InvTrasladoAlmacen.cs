using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvTrasladoAlmacen
{
    public decimal TraIdTraslado { get; set; }

    public decimal? DepIdDepartamento { get; set; }

    public decimal? AlmIdAlmacen { get; set; }

    public string? TraNombreTraslado { get; set; }

    public DateTime? TraFechaTraslado { get; set; }

    public virtual InvAlmacen? AlmIdAlmacenNavigation { get; set; }

    public virtual InvDepartamento? DepIdDepartamentoNavigation { get; set; }
}
