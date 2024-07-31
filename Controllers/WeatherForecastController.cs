//using HIS_API.Models;
//using HIS_API.Models2;
using HIS_API.Models3;
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
//using HIS_API.Models;

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
    //[HttpPost("post1")]
    //public IActionResult F1([FromBody] TipoExamenRequest request)
    //{
    //  try
    //  {
    //    using (var db = new DbHisContext())
    //    {
    //      var nuevoTipoExamen = new HisTipoExaman
    //      {
    //        Nombre = request.NombreAdicional
    //      };

    //      db.HisTipoExamen.Add(nuevoTipoExamen);
    //      db.SaveChanges();
    //    }

    //    return Ok(new { message = $"Tipo de examen '{request.NombreAdicional}' agregado exitosamente." });
    //  }
    //  catch (Exception ex)
    //  {
    //    // Manejo de errores
    //    return StatusCode(500, new { error = $"Error interno del servidor: {ex.Message}" });
    //  }
    //}

    [HttpPost("solicitarExamen")]
    public IActionResult SolicitarExamen([FromBody] SolicitudRequest request)
    {
      try
      {
        using (var db = new DbHis2Context())
        {
          // (1) Llenar HIS_Solicitud
          var nuevaSolicitud = new HisSolicitud
          {
            SolicitudCodigoPaciente = request.SolicitudCodigoPaciente,
            SolicitudCuentaCorriente = request.SolicitudCuentaCorriente,
            SolicitudFecha = DateTime.Now
          };
          db.HisSolicituds.Add(nuevaSolicitud);
          db.SaveChanges();

          // Obtener el ID de la solicitud reci�n creada
          int solicitudId = nuevaSolicitud.SolicitudId;

          // (2) Llenar HIS_Fundamento
          var nuevoFundamento = new HisFundamento
          {
            FundamentoDescripcion = request.FundamentoDescripcion,
            FundamentoSolicitudId = solicitudId
          };
          db.HisFundamentos.Add(nuevoFundamento);
          db.SaveChanges();

          // (3) Llenar HIS_Diagnostico
          var nuevoDiagnostico = new HisDiagnostico
          {
            DiagnosticoDescripcion = request.DiagnosticoDescripcion,
            DiagnosticoSolicitudId = solicitudId
          };
          db.HisDiagnosticos.Add(nuevoDiagnostico);
          db.SaveChanges();

          // (4) Llenar HIS_Examen_Solicitud
          foreach (var examenId in request.ExamenIds)
          {
            var nuevoExamenSolicitud = new HisExamenSolicitud
            {
              ExamSolExamenId = examenId,
              ExamSolSolicitudId = solicitudId
            };
            db.HisExamenSolicituds.Add(nuevoExamenSolicitud);
          }
          db.SaveChanges();

          return Ok(new { message = "Solicitud creada exitosamente." });
        }
      }
      catch (Exception ex)
      {
        // Manejo de errores
        return StatusCode(500, new { error = $"Error interno del servidor: {ex.Message}" });
      }
    }



    [HttpGet("imagenologia")]
    public RenderBtn Imagenologia()
    {
      try
      {
        var valores = new List<Valor>();
        var arrayTipoExamen = new List<TipoExamenBtn>();

        using (var db = new DbHis2Context())
        {
          // Verificar si se obtienen valores
          valores = db.HisValors.Select(v => new Valor()
          {
            configuracion = v.ValorConfiguracionId ?? 0,
            id = v.ValorId,
            nombre = v.ValorNombre ?? string.Empty // Manejo de posible referencia nula
          }).ToList();

          if (!valores.Any())
          {
            throw new Exception("No se encontraron valores en la tabla HisValors.");
          }

          // Ejecutar la consulta SQL y verificar si se obtienen resultados
          var arrayExamenes = db.ExamenView.FromSqlRaw("SELECT * FROM ExamenView").ToList();

          if (!arrayExamenes.Any())
          {
            throw new Exception("No se encontraron ex�menes visibles.");
          }

          var groupedExamenes = arrayExamenes.GroupBy(tipoExamen => tipoExamen.TipoNombre).ToList();

          foreach (var items in groupedExamenes)
          {
            var tipoExamen = new TipoExamenBtn
            {
              NombreUnidad = items.Key ?? string.Empty // Manejo de posible referencia nula
            };
            var arrayRegiones = new List<RegionBtn>();

            foreach (var item in items.GroupBy(region => region.RegionNombre).ToList())
            {
              var region = new RegionBtn
              {
                NombreRegion = item.Key ?? string.Empty // Manejo de posible referencia nula
              };

              var regionInfo = db.HisRegions.FirstOrDefault(r => r.RegionNombre == item.Key);
              region.RutaIcono = regionInfo?.RegionRutaIcono ?? string.Empty; // Manejo de posible referencia nula

              arrayRegiones.Add(region);
              var arrayExamenes2 = new List<string>();

              foreach (var item2 in item.ToList())
              {
                var result = db.HisConfiguracionExamen.Include(c => c.ConfigExamConfiguracion).Where(c => c.ConfigExamExamenId == item2.ExamenId).ToList();
                arrayExamenes2.Add(GetBtnHtml3(valores, result, item2.ExamenNombre ?? string.Empty, item2.ExamenId)); // Manejo de posible referencia nula
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
        return new RenderBtn(); // Devolver un objeto RenderBtn vac�o en lugar de null
      }
    }

    private static string GetBtnHtml3(List<Valor> Valor, List<HisConfiguracionExaman> CXE, string examenNombre, int examenId)
    {
      //if CXE is empty se renderiza un btn sin configuraci�n
      int numConfiguracionesXExamen = CXE.Count;
      string Btn;

      if (numConfiguracionesXExamen == 0)
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='No aplica' data-lateralidad='No aplica'>
            <div class='examen-nombre Radiograf�a'>{examenNombre}</div>
          </div>";
        return Btn;
      }

      else if (numConfiguracionesXExamen == 2)
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='Sin contraste' data-lateralidad='Sin definir'>
            <div class='examen-contraste' title='SIN Contraste'>SC</div>
            <div class='examen-nombre Radiograf�a'>{examenNombre}</div>
            <select class='examen-lateralidad'>
              <option value='Sin definir' disabled selected hidden>LAT.</option>
              <option value='IZQ.'>IZQ.</option>
              <option value='DER.'>DER.</option>
              <option value='BILAT.'>BILAT.</option>
            </select>
          </div>";
        return Btn;
      }

      else if (CXE[0].ConfigExamConfiguracion.ConfiguracionNombre == "contraste")
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='Sin contraste' data-lateralidad='No aplica'>
            <div class='examen-contraste' title='SIN Contraste'>SC</div>
            <div class='examen-nombre Radiograf�a'>{examenNombre}</div>
          </div>";
        return Btn;
      }

      else
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='No aplica' data-lateralidad='Sin definir'>
            <div class='examen-nombre Radiograf�a'>{examenNombre}</div>
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
