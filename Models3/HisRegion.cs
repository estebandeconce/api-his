using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisRegion
{
    public int RegionId { get; set; }

    public string RegionNombre { get; set; } = null!;

    public string? RegionRutaIcono { get; set; }

    public virtual ICollection<HisExaman> HisExamen { get; set; } = new List<HisExaman>();
}
