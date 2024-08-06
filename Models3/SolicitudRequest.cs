namespace HIS_API.Models3
{
  public class SolicitudRequest
  {
    public int SolicitudCodigoPaciente { get; set; }
    public int SolicitudCuentaCorriente { get; set; }
    public string? FundamentoDescripcion { get; set; }
    public string? DiagnosticoDescripcion { get; set; }
    public List<Examen>? Examenes { get; set; }
  }

  public class Examen
  {
    public int Id { get; set; }
    public string? Contraste { get; set; }
    public string? Lateralidad { get; set; }
  }
}
