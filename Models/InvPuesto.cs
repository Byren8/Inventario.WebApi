using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvPuesto
{
    public decimal PueIdPuesto { get; set; }

    public string? PueDescDepto { get; set; }

    public DateTime? PueFechaCreacion { get; set; }

    public virtual ICollection<InvEmpleado> InvEmpleados { get; set; } = new List<InvEmpleado>();
}
