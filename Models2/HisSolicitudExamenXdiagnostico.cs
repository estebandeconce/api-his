using System;
using System.Collections.Generic;

namespace HIS_API.Models2;

public partial class HisSolicitudExamenXdiagnostico
{
    public int? DiagnosticoId { get; set; }

    public string? DiagnosticoDescripcion { get; set; }

    public int SolicitudExamenId { get; set; }

    public virtual HisSolicitudExaman? Diagnostico { get; set; }
}
