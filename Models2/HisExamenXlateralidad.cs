using System;
using System.Collections.Generic;

namespace HIS_API.Models2;

public partial class HisExamenXlateralidad
{
    public int ExaIde { get; set; }

    public int? LatLateralidadId { get; set; }

    public virtual HisExamenXsolicitudExaman ExaIdeNavigation { get; set; } = null!;

    public virtual HisLateralidad? LatLateralidad { get; set; }
}
