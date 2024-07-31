using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class ExamenView2
{
    public int ExamenId { get; set; }

    public string? ExamenCodigoFonasa { get; set; }

    public string? ExamenNombre { get; set; }

    public int RegionId { get; set; }

    public string RegionNombre { get; set; } = null!;

    public int TipoId { get; set; }

    public string? TipoNombre { get; set; }
}
