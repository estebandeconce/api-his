using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisSolicitud
{
    public int SolicitudId { get; set; }

    public int? SolicitudCodigoPaciente { get; set; }

    public int? SolicitudCuentaCorriente { get; set; }

    public DateTime? SolicitudFecha { get; set; }

    public virtual ICollection<HisDiagnostico> HisDiagnosticos { get; set; } = new List<HisDiagnostico>();

    public virtual ICollection<HisExamenSolicitud> HisExamenSolicituds { get; set; } = new List<HisExamenSolicitud>();

    public virtual ICollection<HisFundamento> HisFundamentos { get; set; } = new List<HisFundamento>();
}
