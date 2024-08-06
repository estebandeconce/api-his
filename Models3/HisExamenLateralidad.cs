using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisExamenLateralidad
{
    public int ExamLatExamSolId { get; set; }

    public int? ExamLatLateralidadId { get; set; }

    public virtual HisExamenSolicitud ExamLatExamSol { get; set; } = null!;

    public virtual HisLateralidad? ExamLatLateralidad { get; set; }
}
