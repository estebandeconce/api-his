using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisExamenSolicitud
{
    public int ExamSolId { get; set; }

    public int ExamSolExamenId { get; set; }

    public int ExamSolSolicitudId { get; set; }

    public virtual HisExaman ExamSolExamen { get; set; } = null!;

    public virtual HisSolicitud ExamSolSolicitud { get; set; } = null!;

    public virtual HisContrasteExaman? HisContrasteExaman { get; set; }

    public virtual HisExamenLateralidad? HisExamenLateralidad { get; set; }
}
