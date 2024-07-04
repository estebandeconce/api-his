using System;
using System.Collections.Generic;

namespace HIS_API.Models;

public partial class HisExamenXsolicitudExaman
{
    public int? ExaId { get; set; }

    public int? SolicitudExamenId { get; set; }

    public virtual HisExaman? Exa { get; set; }

    public virtual HisSolicitudExaman? SolicitudExamen { get; set; }
}
