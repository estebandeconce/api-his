using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisConfiguracion
{
    public int ConfiguracionId { get; set; }

    public bool? ConfiguracionEsVigente { get; set; }

    public string? ConfiguracionNombre { get; set; }

    public virtual ICollection<HisConfiguracionExaman> HisConfiguracionExamen { get; set; } = new List<HisConfiguracionExaman>();

    public virtual ICollection<HisValor> HisValors { get; set; } = new List<HisValor>();
}
