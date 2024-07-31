using HIS_API.Models;

namespace HIS_API.Templates
{
  public class ExamenMoedel
  {
    public List<TipoExamen> Examenes { get; set; } = new List<TipoExamen>();
    public List<Valor> Valores { get; set; } = new List<Valor>();
  }

  public class TipoExamenRequest
  {
    public string NombreAdicional { get; set; } = string.Empty;
  }

  public class RenderBtn
  {
    public List<TipoExamenBtn> Examenes { get; set; } = new List<TipoExamenBtn>();
  }

  public class Valor
  {
    public int id { get; set; }
    public string nombre { get; set; } = string.Empty;
    public int configuracion { get; set; }
  }

  public class TipoExamen
  {
    public string NombreUnidad { get; set; } = string.Empty;
    public List<Region> Regiones { get; set; } = new List<Region>();
  }

  public class TipoExamenBtn
  {
    public string NombreUnidad { get; set; } = string.Empty;
    public List<RegionBtn> Regiones { get; set; } = new List<RegionBtn>();
  }

  public class Region
  {
    public string NombreRegion { get; set; } = string.Empty;
    public string RutaIcono { get; set; } = string.Empty;
    public List<Examen> Examenes { get; set; } = new List<Examen>();
  }

  public class RegionBtn
  {
    public string NombreRegion { get; set; } = string.Empty;
    public string RutaIcono { get; set; } = string.Empty;
    public List<string> Btn { get; set; } = new List<string>();
  }

  public class Examen
  {
    public string NombreExamen { get; set; } = string.Empty;
    public List<Configuracion> Configuraciones { get; set; } = new List<Configuracion>();
  }

  public class Configuracion
  {
    public bool esArray { get; set; }
    public bool esRequerido { get; set; }
    public string NombreConfiguracion { get; set; } = string.Empty;
    public int? ValorDefecto { get; set; }
  }
}
