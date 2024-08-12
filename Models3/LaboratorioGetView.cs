using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class LaboratorioGetView
{
    public int ExamenId { get; set; }

    public string? ExamenNombre { get; set; }

    public string? TipoNombre { get; set; }
}
