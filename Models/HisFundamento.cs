using System;
using System.Collections.Generic;

namespace HIS_API.Models;

public partial class HisFundamento
{
    public int FundamentoId { get; set; }

    public string FundamentoDescripcion { get; set; } = null!;

    public int? SolicitudExamenId { get; set; }

    public virtual HisSolicitudExaman? SolicitudExamen { get; set; }
}
