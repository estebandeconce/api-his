using System;
using System.Collections.Generic;

namespace HIS_API.Models;

public partial class HisSolicitudExamenXdiagnostico
{
    public int? SolExamId { get; set; }

    public int? DiagnosticoId { get; set; }

    public virtual HisDiagnostico? Diagnostico { get; set; }

    public virtual HisSolicitudExaman? SolExam { get; set; }
}
