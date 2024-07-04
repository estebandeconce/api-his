using HIS_API.Models;
using HIS_API.Templates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HIS_API.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return Json("Hola");
    }

    [HttpPost]
    public IActionResult Index(string v)
    {
      var arrayTipoExamen = new List<TipoExamen>();

      using (var db = new DbHisContext())
      {
        var arrayExamenes = db.ExamenView.FromSqlRaw("SELECT * FROM ExamenView").GroupBy(tipoExamen => tipoExamen.TipoExNombre).ToList();
        foreach (var items in arrayExamenes)
        {
          var tipoExamen = new TipoExamen();
          tipoExamen.NombreUnidad = items.Key;
          var arrayRegiones = new List<Region>();

          foreach (var item in items.GroupBy(region => region.RegionNombre).ToList())
          {
            var region = new Region();
            region.NombreRegion = item.Key;
            arrayRegiones.Add(region);
            var arrayExamenes2 = new List<Examen>();

            foreach (var item2 in item.ToList())
            {
              var examen = new Examen();
              examen.NombreExamen = item2.ExamenNombre;

              //array de Configuraciones
              var result = db.HisConfiguracionXexamen.Include(c => c.HisConfiguracion).Where(c => c.HisExamenId == item2.ExamenId).ToList();

              if (result.Count > 0)
              {
                var arrayConfiguracion = result.Select(x => new Configuracion()
                {
                  esArray = x.EsArray.HasValue ? x.EsArray.Value : false,
                  esRequerido = x.EsRequerido.HasValue ? x.EsRequerido.Value : false,
                  //valorPorDefecto
                  ValorDefecto = x.ValorPorDefecto,
                  NombreConfiguracion = x.HisConfiguracion.Nombre,
                  //Valores = db.HisValors.Where(v => v.ConfiguracionId == x.HisConfiguracionId).ToList()
                  //Configuracion_id
                  //Examen_id
                });
                examen.Configuraciones = arrayConfiguracion.ToList();
              }
              examen.NombreExamen = item2.ExamenNombre;
              arrayExamenes2.Add(examen);
            }
            region.Examenes = arrayExamenes2;
          }
          tipoExamen.Regiones = arrayRegiones;
          arrayTipoExamen.Add(tipoExamen);
        }
      }
      return Json(arrayTipoExamen);
    }
  }
}
