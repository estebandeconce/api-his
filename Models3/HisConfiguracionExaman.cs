using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisConfiguracionExaman
{
    public int ConfigExamId { get; set; }

    public bool? ConfigExamEsArray { get; set; }

    public bool? ConfigExamEsRequerido { get; set; }

    public short? ConfigExamValorPorDefecto { get; set; }

    public int ConfigExamConfiguracionId { get; set; }

    public int ConfigExamExamenId { get; set; }

    public virtual HisConfiguracion ConfigExamConfiguracion { get; set; } = null!;

    public virtual HisExaman ConfigExamExamen { get; set; } = null!;
}
