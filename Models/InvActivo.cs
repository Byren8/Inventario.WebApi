using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvActivo
{
    public decimal ActIdActivos { get; set; }

    public decimal? AsiIdAsignacion { get; set; }

    public string? ActNombreActivo { get; set; }

    public string? ActDescActivo { get; set; }

    public decimal? ActCantidadActivo { get; set; }

    public DateTime? ActFechaAdquisicion { get; set; }

    public decimal? ActCostoActivo { get; set; }

    public virtual InvAsignacionActivo? AsiIdAsignacionNavigation { get; set; }
}
