using System;
using System.Collections.Generic;

namespace HIS_API.Models;

public partial class HisDiagnostico
{
    public int DiagnosticoId { get; set; }

    public string DiagnosticoDescripcion { get; set; } = null!;
}
