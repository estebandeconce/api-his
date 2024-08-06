using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisTipo
{
    public int TipoId { get; set; }

    public string? TipoNombre { get; set; }

    public virtual ICollection<HisExaman> HisExamen { get; set; } = new List<HisExaman>();
}
