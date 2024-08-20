using HIS_API.Models3;
using HIS_API.Templates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using PuppeteerSharp;
//Data Source=10.5.214.129;Initial Catalog=DB_HIS;Persist Security Info=True;User ID=TIC;Password=Tic***H$p;Encrypt=True;Trust Server Certificate=True
//Scaffold-DbContext "SERVER=10.5.214.129;DATABASE=DB_HIS2;USER ID=rene;PASSWORD=1779;TRUSTED_CONNECTION=false;TRUSTSERVERCERTIFICATE=true;Encrypt=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models3

namespace HIS_API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
  public class WeatherForecastController : ControllerBase
  {
    [HttpGet("getPup")]
    public async Task<IActionResult> GetPup()
    {
      try
      {
        using var db = new DbHis2Context();

        var options = new LaunchOptions
        {
          Headless = true,
        };

        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();

        using var browser = await Puppeteer.LaunchAsync(options);
        using var page = await browser.NewPageAsync();
        using var memoryStream = new MemoryStream();

        // Generar una URL completa
        var url = Url.Action("Solicitud", "Reportes", null, Request.Scheme);

        await page.GoToAsync(url);

        var pdfStream = await page.PdfDataAsync();

        return File(pdfStream, "application/pdf");
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { error = $"Error interno del servidor: {ex.Message}" });
      }
    }


    [HttpPost("postPup")]
    public async Task<IActionResult> PostPup()
    {
      try
      {
        using var db = new DbHis2Context();
        return Ok();
      }
      catch (Exception ex)
      {
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
            throw new Exception("No se encontraron valores en la tabla HIS_VALOR.");
          }

          // Ejecutar la consulta SQL y verificar si se obtienen resultados
          var arrayExamenes = db.ExamenView2s.FromSqlRaw("SELECT * FROM ExamenView2 ORDER BY Tipo_nombre ASC, Region_nombre ASC, Examen_nombre ASC").ToList();

          if (!arrayExamenes.Any())
          {
            throw new Exception("No se encontraron exámenes visibles.");
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
                arrayExamenes2.Add(GetBtnHtml3(valores, result, item2.ExamenNombre ?? string.Empty, item2.ExamenId, item2.ExamenCodigoFonasa)); // Manejo de posible referencia nula
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
        return new RenderBtn(); // Devolver un objeto RenderBtn vacío en lugar de null
      }
    }

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

          // Obtener el ID de la solicitud recién creada
          int solicitudId = nuevaSolicitud.SolicitudId;

          // (2) Llenar HIS_Fundamento
          var nuevoFundamento = new HisFundamento
          {
            FundamentoDescripcion = request.FundamentoDescripcion,
            FundamentoSolicitudId = solicitudId
          };
          db.HisFundamentos.Add(nuevoFundamento);

          // (3) Llenar HIS_Diagnostico
          var nuevoDiagnostico = new HisDiagnostico
          {
            DiagnosticoDescripcion = request.DiagnosticoDescripcion,
            DiagnosticoSolicitudId = solicitudId
          };
          db.HisDiagnosticos.Add(nuevoDiagnostico);

          // (4) Llenar HIS_Examen_Solicitud
          foreach (var examen in request.Examenes)
          {
            var nuevoExamenSolicitud = new HisExamenSolicitud
            {
              ExamSolExamenId = examen.Id,
              ExamSolSolicitudId = solicitudId
            };
            db.HisExamenSolicituds.Add(nuevoExamenSolicitud);
            db.SaveChanges();

            // Obtener el ID del examen-solicitud recién creado
            int examenSolicitudId = nuevoExamenSolicitud.ExamSolId;

            // Manejar contraste
            if (examen.Contraste == "Sin contraste")
            {
              var nuevoContrasteExamen = new HisContrasteExaman
              {
                ContrasExamExamSolId = examenSolicitudId,
                ContrasExamContrasteId = 2
              };
              db.HisContrasteExamen.Add(nuevoContrasteExamen);
            }
            else if (examen.Contraste == "Con contraste")
            {
              var nuevoContrasteExamen = new HisContrasteExaman
              {
                ContrasExamExamSolId = examenSolicitudId,
                ContrasExamContrasteId = 3
              };
              db.HisContrasteExamen.Add(nuevoContrasteExamen);
            }

            // Manejar lateralidad
            if (examen.Lateralidad == "IZQ.")
            {
              var nuevaLateralidadExamen = new HisExamenLateralidad
              {
                ExamLatExamSolId = examenSolicitudId,
                ExamLatLateralidadId = 3
              };
              db.HisExamenLateralidads.Add(nuevaLateralidadExamen);
            }
            else if (examen.Lateralidad == "DER.")
            {
              var nuevaLateralidadExamen = new HisExamenLateralidad
              {
                ExamLatExamSolId = examenSolicitudId,
                ExamLatLateralidadId = 4
              };
              db.HisExamenLateralidads.Add(nuevaLateralidadExamen);
            }
            else if (examen.Lateralidad == "BILAT.")
            {
              var nuevaLateralidadExamen = new HisExamenLateralidad
              {
                ExamLatExamSolId = examenSolicitudId,
                ExamLatLateralidadId = 5
              };
              db.HisExamenLateralidads.Add(nuevaLateralidadExamen);
            }
          }
          db.SaveChanges();

          // Devolver el ID de la solicitud recién creada
          return Ok(new { SolicitudId = solicitudId });
        }
      }
      catch (Exception ex)
      {
        // Manejo de errores
        return StatusCode(500, new { error = $"Error interno del servidor: {ex.Message}" });
      }
    }

    [HttpGet("laboratorio")]
    public IActionResult GetLaboratorio()
    {
      try
      {
        using var db = new DbHis2Context();
        // (1) Hacer la consulta
        var examenes = db.LaboratorioGetViews
            .FromSqlRaw("SELECT * FROM LaboratorioGetView ORDER BY Tipo_nombre ASC, Examen_nombre ASC")
            .ToList();

        // (2) Agrupar los exámenes por tipo
        var groupedExamenes = examenes
            .GroupBy(e => e.TipoNombre)
            .Select(g => new
            {
              Tipo = new
              {
                Nombre = g.Key,
                RutaIcono = db.HisTipos.FirstOrDefault(t => t.TipoNombre == g.Key)?.TipoRutaIcono ?? string.Empty, // Obtener la ruta del icono
                Btn = g.Select(e => GetBtnHtmlLaboratorio(e.ExamenNombre, e.ExamenId, e.TipoNombre)).ToList()
              }
            })
            .ToList();

        // (3) Devolver un JSON
        return Ok(groupedExamenes);
      }
      catch (Exception ex)
      {
        // Manejo de errores
        return StatusCode(500, new { error = $"Error interno del servidor: {ex.Message}" });
      }
    }

    [HttpPost("SolicitarExamenLaboratorio")]
    public IActionResult SolicitarExamenLaboratorio([FromBody] SolicitudRequest request)
    {
      try
      {
        using (var db = new DbHis2Context())
        {
          var nuevaSolicitud = new HisSolicitud
          {
            SolicitudCodigoPaciente = request.SolicitudCodigoPaciente,
            SolicitudCuentaCorriente = request.SolicitudCuentaCorriente,
            SolicitudFecha = DateTime.Now
          };
          db.HisSolicituds.Add(nuevaSolicitud);
          db.SaveChanges();

          int solicitudId = nuevaSolicitud.SolicitudId;

          var nuevoFundamento = new HisFundamento
          {
            FundamentoDescripcion = request.FundamentoDescripcion,
            FundamentoSolicitudId = solicitudId
          };
          db.HisFundamentos.Add(nuevoFundamento);

          var nuevoDiagnostico = new HisDiagnostico
          {
            DiagnosticoDescripcion = request.DiagnosticoDescripcion,
            DiagnosticoSolicitudId = solicitudId
          };
          db.HisDiagnosticos.Add(nuevoDiagnostico);

          foreach (var examen in request.Examenes)
          {
            var nuevoExamenSolicitud = new HisExamenSolicitud
            {
              ExamSolExamenId = examen.Id,
              ExamSolSolicitudId = solicitudId
            };
            db.HisExamenSolicituds.Add(nuevoExamenSolicitud);
            db.SaveChanges();

            // Obtener el ID del examen-solicitud recién creado
            int examenSolicitudId = nuevoExamenSolicitud.ExamSolId;
          }
          db.SaveChanges();

          // Devolver el ID de la solicitud recién creada
          return Ok(solicitudId);
        }
      }
      catch (Exception ex)
      {
        // Manejo de errores
        return StatusCode(500, new { error = $"Error interno del servidor: {ex.Message}" });
      }
    }

    private static string GetBtnHtmlLaboratorio(string examenNombre, int examenId, string tipoNombre)
    {
      string Btn = $@"
          <div class='examen-container' id='id{examenId}' data-tipo='{tipoNombre}' title='{tipoNombre}'>
            <div class='examen-nombre'>{examenNombre}</div>
          </div>";
      return SanitizeHtml(Btn);
    }

    private static string SanitizeHtml(string html)
    {
      return html.Replace("\r\n", "").Replace("  ", "").Trim();
    }


    //Agregar el tipo de examen para que tenga su propio color
    private static string GetBtnHtml3(List<Valor> Valor, List<HisConfiguracionExaman> CXE, string examenNombre, int examenId, string codigoFonasa)
    {
      //if CXE is empty se renderiza un btn sin configuración
      int numConfiguracionesXExamen = CXE.Count;
      string Btn;


      // (*) SIN CONTRASTE && SIN LATERALIDAD
      if (numConfiguracionesXExamen == 0)
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='No aplica' data-lateralidad='No aplica' data-codigo-fonasa='{codigoFonasa}'>
            <div class='examen-nombre Radiografía'>{examenNombre}</div>
          </div>";
        return Btn;
      }

      // (*) CON CONTRASTE && CON BILATERALIDAD OBLIGATORIA
      else if (numConfiguracionesXExamen == 2 && CXE[0].ConfigExamValorPorDefecto == 5)
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='Sin contraste' data-lateralidad='BILAT.' data-codigo-fonasa='{codigoFonasa}'>
            <div class='examen-contraste' title='SIN Contraste'>SC</div>            
            <div class='examen-nombre Radiografía'>{examenNombre}</div>
            <select class='examen-lateralidad'>
              <option value='Sin definir' disabled selected hidden>LAT.</option>
              <option value='BILAT.'>BILAT.</option>
            </select>
          </div>";
        return Btn;
      }

      // (*) CON CONTRASTE && CON LATERALIDAD
      else if (numConfiguracionesXExamen == 2)
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='Sin contraste' data-lateralidad='Sin definir' data-codigo-fonasa='{codigoFonasa}'>
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

      // (*) CON CONTRASTE && SIN LATERALIDAD
      else if (CXE[0].ConfigExamConfiguracion.ConfiguracionNombre == "contraste")
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='Sin contraste' data-lateralidad='No aplica' data-codigo-fonasa='{codigoFonasa}'>
            <div class='examen-contraste' title='SIN Contraste'>SC</div>
            <div class='examen-nombre Radiografía'>{examenNombre}</div>
          </div>";
        return Btn;
      }



      // (*) CON CONTRASTE OBLIGATORIO && SIN LATERALIDAD
      //**********************************
      //**********************************
      //else if (numConfiguracionesXExamen == 1 && CXE[0].ConfigExamValorPorDefecto == 3)
      //{
      //  Btn = $@"
      //    <div class='examen-container' id='{examenId}' data-contraste='Con contraste' data-lateralidad='No aplica' data-codigo-fonasa='{codigoFonasa}'>
      //      <div class='examen-contraste activo' title='CON Contraste'>CC</div>
      //      <div class='examen-nombre Radiografía'>{examenNombre}</div>
      //    </div>";
      //  return Btn;
      //}

      // (*) SIN CONTRASTE && CON BILATERALIDAD OBLIGATORIA
      else if (numConfiguracionesXExamen == 1 && CXE[0].ConfigExamValorPorDefecto == 5)
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='No aplica' data-lateralidad='BILAT.' data-codigo-fonasa='{codigoFonasa}'>
            <div class='examen-nombre Radiografía'>{examenNombre}</div>
            <select class='examen-lateralidad'>
              <option value='Sin definir' disabled selected hidden>LAT.</option>
              <option value='BILAT.'>BILAT.</option>
            </select>
          </div>";
        return Btn;
      }

      // (*) SIN CONTRASTE && CON LATERALIDAD
      else
      {
        Btn = $@"
          <div class='examen-container' id='{examenId}' data-contraste='No aplica' data-lateralidad='Sin definir' data-codigo-fonasa='{codigoFonasa}'>
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
