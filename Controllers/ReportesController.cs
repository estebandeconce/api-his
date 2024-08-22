using HIS_API.Models3;
using Microsoft.AspNetCore.Mvc;

namespace HIS_API.Controllers
{
  public class ReportesController : Controller
  {
    [HttpGet]
    public IActionResult Solicitud(SolicitudPost solicitudPost)
    {
      return View(solicitudPost);
    }
  }
}
