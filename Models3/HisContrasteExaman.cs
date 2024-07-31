using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisContrasteExaman
{
    public int ContrasExamExamSolId { get; set; }

    public int? ContrasExamContrasteId { get; set; }

    public virtual HisContraste? ContrasExamContraste { get; set; }

    public virtual HisExamenSolicitud ContrasExamExamSol { get; set; } = null!;
}
