using System;
using System.Collections.Generic;

namespace HIS_API.Models;

public partial class HisConfiguracion
{
    public int Id { get; set; }

    public bool? EsVigente { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<HisConfiguracionXexaman> HisConfiguracionXexamen { get; set; } = new List<HisConfiguracionXexaman>();

    public virtual ICollection<HisValor> HisValors { get; set; } = new List<HisValor>();
}
