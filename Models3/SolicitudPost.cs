namespace HIS_API.Models3
{
  public class SolicitudPost
  {
    //O R I G E N
    public string ServicioDeSalud { get; set; }
    public string Establecimiento { get; set; }
    public string UnidadSolicitante { get; set; }
    public string FechaDeEmision { get; set; }

    //P A C I E N T E
    public string NombrePaciente { get; set; }
    public int Ficha { get; set; }
    public int CP { get; set; }
    public int CtaCorriente { get; set; }
    public string RUNPaciente { get; set; }
    public string Sexo { get; set; }
    public string FechaDeNacimiento { get; set; }
    //public string Edad { get; set; } La edad se debería calcular

    //C O N T A C T O
    public string Direccion { get; set; }
    public string Comuna { get; set; }
    public string Telefono { get; set; }
    //public string Email { get; set; }

    //D I A G N Ó S T I C O
    public string Diagnostico { get; set; }

    //E X Á M E N E S

    //P R O F E S I O N A L
    public string NombreProfesional { get; set; }
    public string RUNProfesional { get; set; }
  }
}
