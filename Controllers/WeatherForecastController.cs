//using HIS_API.Models;
using HIS_API.Models2;
using HIS_API.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
//using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HIS_API.Controllers
//Data Source=10.5.214.129;Initial Catalog=DB_HIS;Persist Security Info=True;User ID=TIC;Password=Tic***H$p;Encrypt=True;Trust Server Certificate=True
//Scaffold-DbContext "SERVER=10.5.214.129;DATABASE=DB_HIS;USER ID=rene;PASSWORD=1779;TRUSTED_CONNECTION=false;TRUSTSERVERCERTIFICATE=true;Encrypt=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models2 -Force
//Scaffold-DbContext "SERVER=10.5.214.129;DATABASE=DB_HIS2;USER ID=rene;PASSWORD=1779;TRUSTED_CONNECTION=false;TRUSTSERVERCERTIFICATE=true;Encrypt=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models3
{

  [ApiController]
  [Route("[controller]")]
  [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
  public class WeatherForecastController : ControllerBase
  {
    [HttpPost("post1")]
    public IActionResult F1([FromBody] TipoExamenRequest request)
    {
      try
      {
        using (var db = new DbHisContext())
        {
          var nuevoTipoExamen = new HisTipoExaman
          {
            Nombre = request.NombreAdicional
          };

          db.HisTipoExamen.Add(nuevoTipoExamen);
          db.SaveChanges();
        }

        return Ok(new { message = $"Tipo de examen '{request.NombreAdicional}' agregado exitosamente." });
      }
      catch (Exception ex)
      {
        // Manejo de errores
        return StatusCode(500, new { error = $"Error interno del servidor: {ex.Message}" });
      }
    }

    [HttpGet("get2")]
    public RenderBtn Get2()
    {
      try
      {
        var valores = new List<Valor>();
        var arrayTipoExamen = new List<TipoExamenBtn>();

        using (var db = new DbHisContext())
        {
          // Verificar si se obtienen valores
          valores = db.HisValors.Select(v => new Valor()
          {
            configuracion = v.Configuracion.Id,
            id = v.Id,
            nombre = v.Nombre
          }).ToList();

          if (!valores.Any())
          {
            throw new Exception("No se encontraron valores en la tabla HisValors.");
          }

          // Ejecutar la consulta SQL y verificar si se obtienen resultados
          var arrayExamenes = db.ExamenView.FromSqlRaw("SELECT Ex.id AS ExamenId, Ex.codigoFonasa, Ex.nombre AS ExamenNombre, Reg.id AS RegionId, Reg.nombre AS RegionNombre, TE.id AS TipoExId, TE.nombre AS TipoExNombre FROM dbo.HIS_Examen AS Ex INNER JOIN dbo.HIS_Region AS Reg ON Ex.Region_id = Reg.id INNER JOIN dbo.HIS_TipoExamen AS TE ON Ex.TipoExamen_id = TE.id WHERE Ex.visible = 1").ToList();

          if (!arrayExamenes.Any())
          {
            throw new Exception("No se encontraron exámenes visibles.");
          }

          var groupedExamenes = arrayExamenes.GroupBy(tipoExamen => tipoExamen.TipoExNombre).ToList();

          foreach (var items in groupedExamenes)
          {
            var tipoExamen = new TipoExamenBtn
            {
              NombreUnidad = items.Key
            };
            var arrayRegiones = new List<RegionBtn>();

            foreach (var item in items.GroupBy(region => region.RegionNombre).ToList())
            {
              var region = new RegionBtn
              {
                NombreRegion = item.Key
              };

              var regionInfo = db.HisRegions.FirstOrDefault(r => r.Nombre == item.Key);
              region.RutaIcono = regionInfo?.RutaIcono;

              arrayRegiones.Add(region);
              var arrayExamenes2 = new List<string>();

              foreach (var item2 in item.ToList())
              {
                var result = db.HisConfiguracionXexamen.Include(c => c.HisConfiguracion).Where(c => c.HisExamenId == item2.ExamenId).ToList();
                arrayExamenes2.Add(GetBtnHtml(valores, result, item2.ExamenNombre, item2.ExamenId));
              }
              region.Btn = arrayExamenes2;
            }
            tipoExamen.Regiones = arrayRegiones;
            arrayTipoExamen.Add(tipoExamen);
          }
        }

        return new RenderBtn()
        {
          Examenes = arrayTipoExamen,
        };
      }
      catch (Exception ex)
      {
        // Log the exception (optional)
        Console.WriteLine(ex.Message);
        return null;
      }
    }




    //[HttpGet("get3")]
    //public RenderBtn Get3()
    //{
    //  try
    //  {
    //    var valores = new List<Valor>();
    //    var arrayTipoExamen = new List<TipoExamenBtn>();

    //    using (var db = new DbHisContext())
    //    {

    //      valores = [.. db.HisValors.Select(v => new Valor()
    //      {
    //        configuracion = v.ConfiguracionId,
    //        id = v.Id,
    //        nombre = v.Nombre
    //      })];
    //      var arrayExamenes = db.ExamenView.FromSqlRaw("SELECT * FROM ExamenView").GroupBy(tipoExamen => tipoExamen.TipoExNombre).ToList();

    //      foreach (var items in arrayExamenes)
    //      {

    //        //primer nivel 
    //        var tipoExamen = new TipoExamenBtn
    //        {
    //          NombreUnidad = items.Key
    //        };
    //        var arrayRegiones = new List<RegionBtn>();

    //        foreach (var item in items.GroupBy(region => region.RegionNombre).ToList())
    //        {
    //          //segundo nivel

    //          var region = new RegionBtn
    //          {
    //            NombreRegion = item.Key
    //          };

    //          // Aquí se carga la rutaIcono para la región actual
    //          var regionInfo = db.HisRegions.FirstOrDefault(r => r.Nombre == item.Key);
    //          region.RutaIcono = regionInfo?.RutaIcono; // Asegúrate de que tu modelo RegionBtn tenga una propiedad RutaIcono

    //          arrayRegiones.Add(region);
    //          var arrayExamenes2 = new List<string>();

    //          foreach (var item2 in item.ToList())
    //          {
    //            var result = db.HisConfiguracionXexamen.Include(c => c.HisConfiguracion).Where(c => c.HisExamenId == item2.ExamenId).ToList();
    //            arrayExamenes2.Add(GetBtnHtml(valores, result, item2.ExamenNombre, item2.ExamenId));
    //          }
    //          region.Btn = arrayExamenes2;

    //        }
    //        tipoExamen.Regiones = arrayRegiones;
    //        arrayTipoExamen.Add(tipoExamen);
    //      }
    //    }

    //    return new RenderBtn()
    //    {
    //      Examenes = arrayTipoExamen,

    //    };

    //  }
    //  catch (Exception ex)
    //  {

    //    return null;
    //  }
    //}

    private static string GetBtnHtml(List<Valor> Valor, List<HisConfiguracionXexaman> CXE, string examenNombre, int examenId)
    {
      //if CXE is empty se renderiza un btn sin configuración
      int numConfiguracionesXExamen = CXE.Count;
      string Btn;

      if (numConfiguracionesXExamen == 0)
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='No aplica' data-lateralidad='No aplica'>
            <div class='examen-nombre Radiografía'>{examenNombre}</div>
          </div>";
        return Btn;
      }

      else if (numConfiguracionesXExamen == 2)
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='Sin contraste' data-lateralidad='Sin definir'>
            <div class='examen-contraste' title='SIN Contraste'>SC</div>
            <div class='examen-nombre Radiografía'>{examenNombre}</div>
            <select class='examen-lateralidad'>
              <option value='Sin definir' disabled selected hidden>LAT.</option>
              <option value='IZQ.'>IZQ.</option>
              <option value='DER.'>DER.</option>
              <option value='BILAT.'>BILAT.</option>
            </select>
          </div>";
        return Btn;
      }

      else if (CXE[0].HisConfiguracion.Nombre == "contraste")
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='Sin contraste' data-lateralidad='No aplica'>
            <div class='examen-contraste' title='SIN Contraste'>SC</div>
            <div class='examen-nombre Radiografía'>{examenNombre}</div>
          </div>";
        return Btn;
      }

      else
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='No aplica' data-lateralidad='Sin definir'>
            <div class='examen-nombre Radiografía'>{examenNombre}</div>
            <select class='examen-lateralidad'>
              <option value='Sin definir' disabled selected hidden>LAT.</option>
              <option value='IZQ.'>IZQ.</option>
              <option value='DER.'>DER.</option>
              <option value='BILAT.'>BILAT.</option>
            </select>
          </div>";
        return Btn;
      }
    }
  }
}
