using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisDiagnostico
{
    public int DiagnosticoId { get; set; }

    public string? DiagnosticoDescripcion { get; set; }

    public int DiagnosticoSolicitudId { get; set; }

    public virtual HisSolicitud DiagnosticoSolicitud { get; set; } = null!;
}
