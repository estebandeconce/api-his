using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class HisFundamento
{
    public int FundamentoId { get; set; }

    public string? FundamentoDescripcion { get; set; }

    public int FundamentoSolicitudId { get; set; }

    public virtual HisSolicitud FundamentoSolicitud { get; set; } = null!;
}
