using HIS_API.Models3;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace HIS_API.Controllers
{
  public class ReportesController : Controller
  {
    [HttpGet]
    public IActionResult Solicitud(string model)
    {
      var solicitudPost = JsonSerializer.Deserialize<SolicitudPost>(model);

      if (solicitudPost.Origen == "IMAGENOLOGÍA")
      {
        foreach (var examen in solicitudPost.Examenes)
        {
          examen.Contraste = examen.Contraste == "Con contraste" ? "Sí" : "No";
          examen.ExamenTipo = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(examen.ExamenTipo.Split(' ')[0].ToLower());
          examen.ExamenNombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(examen.ExamenNombre.ToLower());
          examen.Lateralidad = examen.Lateralidad switch
          { 
            "No aplica" => "No",
            "DER." => "Derecha",
            "IZQ." => "Izquierda",
            "BILAT." => "Bilateral",
            _ => examen.Lateralidad
          };
        }
      }

      solicitudPost.EdadString = CalcularEdad(solicitudPost.FechaDeNacimiento);
      return View(solicitudPost);
    }

    private static string CalcularEdad(DateTime fechaDeNacimiento)
    {
      var hoy = DateTime.Today;
      var edadAnios = hoy.Year - fechaDeNacimiento.Year;
      var edadMeses = hoy.Month - fechaDeNacimiento.Month;

      if (fechaDeNacimiento.Date > hoy.AddYears(-edadAnios))
      {
        edadAnios--;
        edadMeses += 12;
      }

      if (edadMeses < 0)
      {
        edadAnios--;
        edadMeses += 12;
      }

      string aniosTexto = edadAnios == 1 ? "año" : "años";
      string mesesTexto = edadMeses == 1 ? "mes" : "meses";

      return $"{edadAnios} {aniosTexto} y {edadMeses} {mesesTexto}";
    }
  }
}
