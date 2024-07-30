using System;
using System.Collections.Generic;

namespace HIS_API.Models2;

public partial class HisRegion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? RutaIcono { get; set; }

    public virtual ICollection<HisExaman> HisExamen { get; set; } = new List<HisExaman>();
}
