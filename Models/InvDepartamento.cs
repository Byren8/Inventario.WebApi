using System;
using System.Collections.Generic;

namespace Inventario.DataAcces_.Models;

public partial class InvDepartamento
{
    public decimal DepIdDepartamento { get; set; }

    public decimal? MunIdMunicipio { get; set; }

    public string? DepNombre { get; set; }

    public decimal? DepCodigoPostal { get; set; }

    public virtual ICollection<InvTrasladoAlmacen> InvTrasladoAlmacens { get; set; } = new List<InvTrasladoAlmacen>();

    public virtual InvMunicipio? MunIdMunicipioNavigation { get; set; }
}
