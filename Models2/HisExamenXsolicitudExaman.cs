using System;
using System.Collections.Generic;

namespace HIS_API.Models2;

public partial class HisExamenXsolicitudExaman
{
    public int? ExaId { get; set; }

    public int? SolicitudExamenId { get; set; }

    public int Id { get; set; }

    public virtual HisExaman? Exa { get; set; }

    public virtual HisContrasteXexaman? HisContrasteXexaman { get; set; }

    public virtual HisExamenXlateralidad? HisExamenXlateralidad { get; set; }

    public virtual HisSolicitudExaman? SolicitudExamen { get; set; }
}
