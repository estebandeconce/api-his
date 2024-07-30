using System;
using System.Collections.Generic;

namespace HIS_API.Models2;

public partial class HisConfiguracionXexaman
{
    public int Id { get; set; }

    public bool? EsArray { get; set; }

    public bool? EsRequerido { get; set; }

    public short? ValorPorDefecto { get; set; }

    public int ConfiguracionId { get; set; }

    public int ExamenId { get; set; }

    public virtual HisConfiguracion Configuracion { get; set; } = null!;

    public virtual HisExaman Examen { get; set; } = null!;
}
