using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class SolicitudView
{
    public int SolicitudId { get; set; }

    public int? CódigoPaciente { get; set; }

    public int? CtaCorriente { get; set; }

    public string? Fecha { get; set; }

    public string? Fundamento { get; set; }

    public string? Descripción { get; set; }

    public int? DetalleSolicitudId { get; set; }

    public string? NombreExamen { get; set; }

    public string? Lateralidad { get; set; }

    public string? Contraste { get; set; }
}
