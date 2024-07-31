namespace HIS_API.Models3
{
  public class SolicitudRequest
  {
    public int SolicitudCodigoPaciente { get; set; }
    public int SolicitudCuentaCorriente { get; set; }
    public string FundamentoDescripcion { get; set; }
    public string DiagnosticoDescripcion { get; set; }
    public List<int> ExamenIds { get; set; }
  }
}
