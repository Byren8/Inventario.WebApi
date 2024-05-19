using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvMunicipio
{
    public decimal MunIdMunicipio { get; set; }

    public string? MunDescMunicipio { get; set; }

    public virtual ICollection<InvDepartamento> InvDepartamentos { get; set; } = new List<InvDepartamento>();
}
