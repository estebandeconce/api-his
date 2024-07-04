using HIS_API.Models;
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

namespace HIS_API.Controllers
//Data Source=10.5.214.129;Initial Catalog=DB_HIS;Persist Security Info=True;User ID=TIC;Password=Tic***H$p;Encrypt=True;Trust Server Certificate=True
//Scaffold-DbContext "data source=10.5.214.129;initial catalog=db_his;user id=rene;password=1779;Trusted_Connection=False;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
{

  [ApiController]
  [Route("[controller]")]
  [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
          "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
      };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }

    [HttpGet("get1")]
    public ExamenMoedel Get1()
    {
      try
      {
        var valores = new List<Valor>();
        var arrayTipoExamen = new List<TipoExamen>();

        using (var db = new DbHisContext())
        {

          valores = db.HisValors.Select(v => new Valor()
          {
            configuracion = v.ConfiguracionId,
            id = v.Id,
            nombre = v.Nombre
          }).ToList();
          var arrayExamenes = db.ExamenView.FromSqlRaw("SELECT * FROM ExamenView").GroupBy(tipoExamen => tipoExamen.TipoExNombre).ToList();

          foreach (var items in arrayExamenes)
          {

            //primer nivel 
            var tipoExamen = new TipoExamen();
            tipoExamen.NombreUnidad = items.Key;
            var arrayRegiones = new List<Region>();

            foreach (var item in items.GroupBy(region => region.RegionNombre).ToList())
            {
              //segundo nivel

              var region = new Region();
              region.NombreRegion = item.Key;
              arrayRegiones.Add(region);
              var arrayExamenes2 = new List<Examen>();

              foreach (var item2 in item.ToList())
              {

                //tercer nivel
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

                    //Configuracion_id
                    //Examen_id


                  });
                  examen.Configuraciones = arrayConfiguracion.ToList();

                }


                arrayExamenes2.Add(examen);
              }


              region.Examenes = arrayExamenes2;
            }
            tipoExamen.Regiones = arrayRegiones;
            arrayTipoExamen.Add(tipoExamen);
          }
        }
        //var examenesJson = JsonSerializer.Serialize(arrayTipoExamen, new JsonSerializerOptions() {ReferenceHandler = ReferenceHandler.Preserve });
        return new ExamenMoedel()
        {
          Examenes = arrayTipoExamen,
          Valores = valores
        };

      }
      catch (Exception ex)
      {

        return null;
      }

    }

    [HttpGet("get2")]
    public ExamenMoedel Get2()
    {
      try
      {
        var valores = new List<Valor>();
        var arrayTipoExamen = new List<TipoExamen>();

        using (var db = new DbHisContext())
        {

          valores = db.HisValors.Select(v => new Valor()
          {
            configuracion = v.ConfiguracionId,
            id = v.Id,
            nombre = v.Nombre
          }).ToList();
          var arrayExamenes = db.ExamenView.FromSqlRaw("SELECT * FROM ExamenView").GroupBy(tipoExamen => tipoExamen.TipoExNombre).ToList();

          foreach (var items in arrayExamenes)
          {

            //primer nivel 
            var tipoExamen = new TipoExamen();
            tipoExamen.NombreUnidad = items.Key;
            var arrayRegiones = new List<Region>();

            foreach (var item in items.GroupBy(region => region.RegionNombre).ToList())
            {
              //segundo nivel

              var region = new Region();
              region.NombreRegion = item.Key;
              arrayRegiones.Add(region);
              var arrayExamenes2 = new List<Examen>();

              foreach (var item2 in item.ToList())
              {

                //tercer nivel
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

                    //Configuracion_id
                    //Examen_id


                  });
                  examen.Configuraciones = arrayConfiguracion.ToList();

                }


                arrayExamenes2.Add(examen);
              }


              region.Examenes = arrayExamenes2;
            }
            tipoExamen.Regiones = arrayRegiones;
            arrayTipoExamen.Add(tipoExamen);
          }
        }
        //var examenesJson = JsonSerializer.Serialize(arrayTipoExamen, new JsonSerializerOptions() {ReferenceHandler = ReferenceHandler.Preserve });
        return new ExamenMoedel()
        {
          Examenes = arrayTipoExamen,
          Valores = valores
        };

      }
      catch (Exception ex)
      {

        return null;
      }

    }

    [HttpGet("get3")]
    public RenderBtn Get3()
    {
      try
      {
        var valores = new List<Valor>();
        var arrayTipoExamen = new List<TipoExamenBtn>();

        using (var db = new DbHisContext())
        {

          valores = db.HisValors.Select(v => new Valor()
          {
            configuracion = v.ConfiguracionId,
            id = v.Id,
            nombre = v.Nombre
          }).ToList();
          var arrayExamenes = db.ExamenView.FromSqlRaw("SELECT * FROM ExamenView").GroupBy(tipoExamen => tipoExamen.TipoExNombre).ToList();

          foreach (var items in arrayExamenes)
          {

            //primer nivel 
            var tipoExamen = new TipoExamenBtn();
            tipoExamen.NombreUnidad = items.Key;
            var arrayRegiones = new List<RegionBtn>();

            foreach (var item in items.GroupBy(region => region.RegionNombre).ToList())
            {
              //segundo nivel

              var region = new RegionBtn();
              region.NombreRegion = item.Key;
              arrayRegiones.Add(region);
              var arrayExamenes2 = new List<string>();

              foreach (var item2 in item.ToList())
              {
                var result = db.HisConfiguracionXexamen.Include(c => c.HisConfiguracion).Where(c => c.HisExamenId == item2.ExamenId).ToList();
                arrayExamenes2.Add(getBtnHtml(valores, result, item2.ExamenNombre, item2.ExamenId));
              }
              region.Btn = arrayExamenes2;

            }
            tipoExamen.Regiones = arrayRegiones;
            arrayTipoExamen.Add(tipoExamen);
          }
        }
        //var examenesJson = JsonSerializer.Serialize(arrayTipoExamen, new JsonSerializerOptions() {ReferenceHandler = ReferenceHandler.Preserve });
        return new RenderBtn()
        {
          Examenes = arrayTipoExamen,

        };

      }
      catch (Exception ex)
      {

        return null;
      }

    }


    [HttpPost(Name = "PostWeatherForecast")]
    public RenderBtn Post()
    {
      try
      {
        var valores = new List<Valor>();
        var arrayTipoExamen = new List<TipoExamenBtn>();

        using (var db = new DbHisContext())
        {

          valores = db.HisValors.Select(v => new Valor()
          {
            configuracion = v.ConfiguracionId,
            id = v.Id,
            nombre = v.Nombre
          }).ToList();
          var arrayExamenes = db.ExamenView.FromSqlRaw("SELECT * FROM ExamenView").GroupBy(tipoExamen => tipoExamen.TipoExNombre).ToList();

          foreach (var items in arrayExamenes)
          {

            //primer nivel 
            var tipoExamen = new TipoExamenBtn();
            tipoExamen.NombreUnidad = items.Key;
            var arrayRegiones = new List<RegionBtn>();

            foreach (var item in items.GroupBy(region => region.RegionNombre).ToList())
            {
              //segundo nivel

              var region = new RegionBtn();
              region.NombreRegion = item.Key;
              arrayRegiones.Add(region);
              var arrayExamenes2 = new List<string>();

              foreach (var item2 in item.ToList())
              {
                var result = db.HisConfiguracionXexamen.Include(c => c.HisConfiguracion).Where(c => c.HisExamenId == item2.ExamenId).ToList();
                arrayExamenes2.Add(getBtnHtml(valores, result, item2.ExamenNombre, item2.ExamenId));
              }
              region.Btn = arrayExamenes2;

            }
            tipoExamen.Regiones = arrayRegiones;
            arrayTipoExamen.Add(tipoExamen);
          }
        }
        //var examenesJson = JsonSerializer.Serialize(arrayTipoExamen, new JsonSerializerOptions() {ReferenceHandler = ReferenceHandler.Preserve });
        return new RenderBtn()
        {
          Examenes = arrayTipoExamen,

        };

      }
      catch (Exception ex)
      {

        return null;
      }

    }

    private string getBtnHtml(List<Valor> Valor, List<HisConfiguracionXexaman> CXE, string examenNombre, int examenId)
    {
      var Btn = $"<div class='examen-container' id='{examenId}' data-contraste='No aplica' data-lateralidad='No aplica'> <div class='examen-nombre Radiografía'>{examenNombre} </div></div>";
      return Btn;
    }

    /* CON Contraste, CON Lateralidad
     <div class="examen-container" id="1" data-contraste="Sin contraste" data-lateralidad="Sin definir">
              
        <div class="examen-contraste" title="SIN Contraste">SC</div>
              
      <div class="examen-nombre Radiografía">
        RX CRÁNEO AP Y LATERAL
      </div>
              
            <select class="examen-lateralidad">
                <option value="Sin definir" disabled="" selected="" hidden="">LAT.</option>
                <option value="IZQ.">IZQ.</option>
                <option value="DER.">DER.</option>
                <option value="BILAT.">BILAT.</option>
            </select>
            </div>
     */

    /* CON Contraste, SIN Lateralidad
    <div class="examen-container" id="2" data-contraste="Sin contraste" data-lateralidad="No aplica">

        <div class="examen-contraste" title="SIN Contraste">SC</div>

      <div class="examen-nombre Radiografía">
        RX CRÁNEO AP, LATERAL Y TOWNE
      </div>

            </div>

     */

    /* SIN Contraste, CON Lateralidad
    <div class="examen-container" id="3" data-contraste="No aplica" data-lateralidad="Sin definir">


      <div class="examen-nombre Radiografía">
        RX DE HUESOS PROPIOS NASALES
      </div>

            <select class="examen-lateralidad">
                <option value="Sin definir" disabled="" selected="" hidden="">LAT.</option>
                <option value="IZQ.">IZQ.</option>
                <option value="DER.">DER.</option>
                <option value="BILAT.">BILAT.</option>
            </select>
            </div>

     */

    /* SIN Contraste, SIN Lateralidad
     <div class="examen-container" id="4" data-contraste="No aplica" data-lateralidad="No aplica">


          <div class="examen-nombre Radiografía">
            RX ARCO CIGOMÁTICO
          </div>

                </div>
     */
  }
}