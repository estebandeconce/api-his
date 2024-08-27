namespace HIS_API.Models3
{
  public class SolicitudPost
  {
    //O R I G E N
    public string Origen { get; set; }
    public string ServicioDeSalud { get; set; }
    public string Establecimiento { get; set; }
    public string UnidadSolicitante { get; set; }
    public DateTime FechaDeEmision { get; set; }
    public int SolicitudId { get; set; }

    //P A C I E N T E
    public string NombrePaciente { get; set; }
    public int Ficha { get; set; }
    public int CP { get; set; }
    public int CtaCorriente { get; set; }
    public string RUNPaciente { get; set; }
    public string Sexo { get; set; }
    public DateTime FechaDeNacimiento { get; set; }
    public string EdadString { get; set; }

    //C O N T A C T O
    public string Direccion { get; set; }
    public string Comuna { get; set; }
    public string Telefono { get; set; }

    //D I A G N Ó S T I C O
    public string Diagnostico { get; set; }
    public string Fundamento { get; set; }

    //E X Á M E N E S
    public virtual ICollection<Examen> Examenes { get; set; } = [];
    //public List<Examen2> Examenes2 { get; set; }

    //P R O F E S I O N A L
    public string NombreProfesional { get; set; }
    public string RUNProfesional { get; set; }
  }

  public class Examen
  {
    public int ExamenCodigoFonasa { get; set; }
    public int ExamenId { get; set; }
    public string ExamenNombre { get; set; }
    public string ExamenTipo { get; set; }
    public string Contraste { get; set; } // Propiedad de Imagenología
    public string Lateralidad { get; set; } // Propiedad de Imagenología
  }
}
