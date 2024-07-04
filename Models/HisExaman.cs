using System;
using System.Collections.Generic;

namespace HIS_API.Models;

public partial class HisExaman
{
    public int Id { get; set; }

    public string? CodigoFonasa { get; set; }

    public string Nombre { get; set; } = null!;

    public int RegionId { get; set; }

    public int TipoExamenId { get; set; }

    public virtual HisRegion Region { get; set; } = null!;

    public virtual HisTipoExaman TipoExamen { get; set; } = null!;
}
