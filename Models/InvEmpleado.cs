using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvEmpleado
{
    public decimal EmpIdEmpleado { get; set; }

    public decimal? PueIdPuesto { get; set; }

    public decimal? EmpTelefono { get; set; }

    public string? EmpNombre { get; set; }

    public string? EmpApellido { get; set; }

    public decimal? EmpCodigoEmpleado { get; set; }

    public virtual ICollection<InvAsignacionActivo> InvAsignacionActivos { get; set; } = new List<InvAsignacionActivo>();

    public virtual InvPuesto? PueIdPuestoNavigation { get; set; }
}
