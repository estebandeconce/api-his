namespace HIS_API.Models3
{
  public class Examen2
  {
    public int IdExamen { get; set; }
    public string NombreExamen { get; set; }
    public string TipoExamen { get; set; }
    public string Contraste { get; set; } // Propiedad adicional
    public string Lateralidad { get; set; } // Propiedad adicional
  }
}
