using System;
using System.Collections.Generic;

namespace HIS_API.Models3;

public partial class SolicitudDetalleView
{
    public int SolId { get; set; }

    public int? CódPaciente { get; set; }

    public int? CtaCorriente { get; set; }

    public string? Fecha { get; set; }

    public string? Funda { get; set; }

    public int? FundaId { get; set; }

    public int? FxsId { get; set; }

    public string? Descrip { get; set; }

    public int? DescripId { get; set; }

    public int? DxsId { get; set; }

    public int? DetalleSolId { get; set; }

    public string? NombreExamen { get; set; }

    public string? Lateralidad { get; set; }

    public string? Contraste { get; set; }

    public int? ExamenLateralidadId { get; set; }

    public int? ExamenContrasteId { get; set; }
}
