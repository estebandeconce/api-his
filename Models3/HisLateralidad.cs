using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisLateralidad
{
    public int LateralidadId { get; set; }

    public string LateralidadValor { get; set; } = null!;

    public virtual ICollection<HisExamenLateralidad> HisExamenLateralidads { get; set; } = new List<HisExamenLateralidad>();
}
