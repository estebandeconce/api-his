namespace HIS_API.Models
{
  public class ExamenView
  {
    public int ExamenId { get; set; }
    public string? CodigoFonasa { get; set; }
    public string? ExamenNombre { get; set; }
    public int RegionId { get; set; }
    public string? RegionNombre { get; set; }
    public int TipoExId { get; set; }
    public string? TipoExNombre { get; set; }
  }
}
