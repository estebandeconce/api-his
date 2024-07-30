using System;
using System.Collections.Generic;

namespace HIS_API.Models;

public partial class HisSolicitudExaman
{
    public int SolExamId { get; set; }

    public int SolExamDiagnosticoId { get; set; }

    public int? Cp { get; set; }

    public DateTime? Fecha { get; set; }

    public int? Ctacte { get; set; }

    public virtual ICollection<HisFundamento> HisFundamentos { get; set; } = [];
}
