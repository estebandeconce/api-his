using System;
using System.Collections.Generic;

namespace HIS_API.Models;

public partial class HisValor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int ConfiguracionId { get; set; }

    public virtual HisConfiguracion Configuracion { get; set; } = null!;
}
