using System;
using System.Collections.Generic;

namespace HIS_API.Models2;

public partial class HisExaman
{
    public int Id { get; set; }

    public string? CodigoFonasa { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Visible { get; set; }

    public int RegionId { get; set; }

    public int TipoExamenId { get; set; }

    public virtual ICollection<HisConfiguracionXexaman> HisConfiguracionXexamen { get; set; } = new List<HisConfiguracionXexaman>();

    public virtual ICollection<HisExamenXsolicitudExaman> HisExamenXsolicitudExamen { get; set; } = new List<HisExamenXsolicitudExaman>();

    public virtual HisRegion Region { get; set; } = null!;

    public virtual HisTipoExaman TipoExamen { get; set; } = null!;
}
