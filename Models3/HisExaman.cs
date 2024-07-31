using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisExaman
{
    public int ExamenId { get; set; }

    public string? ExamenCodigoFonasa { get; set; }

    public string? ExamenNombre { get; set; }

    public bool? ExamenEsVisible { get; set; }

    public int ExamenRegionId { get; set; }

    public int ExamenTipoId { get; set; }

    public virtual HisRegion ExamenRegion { get; set; } = null!;

    public virtual HisTipo ExamenTipo { get; set; } = null!;

    public virtual ICollection<HisConfiguracionExaman> HisConfiguracionExamen { get; set; } = new List<HisConfiguracionExaman>();

    public virtual ICollection<HisExamenSolicitud> HisExamenSolicituds { get; set; } = new List<HisExamenSolicitud>();
}
