using HIS_API.Models;

namespace HIS_API.Templates
{
  public class ExamenMoedel
  {
    public List<TipoExamen> Examenes { get; set; }
    public List<Valor> Valores { get; set; }
  }

  public class TipoExamenRequest
  {
    public string NombreAdicional { get; set; }
  }

  public class RenderBtn
  {
    public List<TipoExamenBtn> Examenes { get; set; }
  
  }

  public class Valor
  {
    public int id { get; set; }
    public string nombre { get; set; }
    public int configuracion { get; set; }
  }

  public class TipoExamen
  {
    public string NombreUnidad { get; set; }
    public List<Region> Regiones { get; set; }
  }
  public class TipoExamenBtn
  {
    public string NombreUnidad { get; set; }
    public List<RegionBtn> Regiones { get; set; }
  }

  public class Region
  {
    public string NombreRegion { get; set; }
    public string RutaIcono { get; set; }
    public List<Examen> Examenes { get; set; }
  }
  public class RegionBtn
  {
    public string NombreRegion { get; set; }
    public string RutaIcono { get; set; }
    public List<string> Btn { get; set; }
  }

  public class Examen
  {
    public string NombreExamen { get; set; }
    public List<Configuracion> Configuraciones { get; set; }
  }

  public class Configuracion
  {
    public bool esArray { get; set; }
    public bool esRequerido { get; set; }
    public string NombreConfiguracion { get; set; }
    public int? ValorDefecto { get; set; }
  }
}
