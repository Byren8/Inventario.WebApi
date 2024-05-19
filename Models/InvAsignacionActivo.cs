using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvAsignacionActivo
{
    public decimal AsiIdAsignacion { get; set; }

    public decimal? EmpIdEmpleado { get; set; }

    public DateTime? AsiFechaAsignacion { get; set; }

    public virtual InvEmpleado? EmpIdEmpleadoNavigation { get; set; }

    public virtual ICollection<InvActivo> InvActivos { get; set; } = new List<InvActivo>();
}
