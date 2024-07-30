using System;
using System.Collections.Generic;

namespace HIS_API.Models2;

public partial class HisLateralidad
{
    public int Id { get; set; }

    public string Valor { get; set; } = null!;

    public virtual ICollection<HisExamenXlateralidad> HisExamenXlateralidads { get; set; } = new List<HisExamenXlateralidad>();
}
