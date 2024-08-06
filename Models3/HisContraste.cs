using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisContraste
{
    public int ContrasteId { get; set; }

    public string ContrasteValor { get; set; } = null!;

    public virtual ICollection<HisContrasteExaman> HisContrasteExamen { get; set; } = new List<HisContrasteExaman>();
}
