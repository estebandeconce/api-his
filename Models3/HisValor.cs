using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisValor
{
    public int ValorId { get; set; }

    public string? ValorNombre { get; set; }

    public int? ValorConfiguracionId { get; set; }

    public virtual HisConfiguracion? ValorConfiguracion { get; set; }
}
