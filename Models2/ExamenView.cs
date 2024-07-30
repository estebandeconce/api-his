using System;
using System.Collections.Generic;

namespace HIS_API.Models2;

public partial class ExamenView
{
    public int ExamenId { get; set; }

    public string? CodigoFonasa { get; set; }

    public string ExamenNombre { get; set; } = null!;

    public int RegionId { get; set; }

    public string RegionNombre { get; set; } = null!;

    public int TipoExId { get; set; }

    public string TipoExNombre { get; set; } = null!;
}
