using System;
using System.Collections.Generic;

namespace HIS_API.Models;

public partial class HisContrasteXexaman
{
    public int? ExaIde { get; set; }

    public int? ContrasteId { get; set; }

    public virtual HisContraste? Contraste { get; set; }

    public virtual HisExaman? ExaIdeNavigation { get; set; }
}
