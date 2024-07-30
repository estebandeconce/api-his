using System;
using System.Collections.Generic;

namespace HIS_API.Models2;

public partial class HisContraste
{
    public int Id { get; set; }

    public string Valor { get; set; } = null!;

    public virtual ICollection<HisContrasteXexaman> HisContrasteXexamen { get; set; } = new List<HisContrasteXexaman>();
}
