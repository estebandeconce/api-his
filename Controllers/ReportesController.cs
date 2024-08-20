using Microsoft.AspNetCore.Mvc;

namespace HIS_API.Controllers
{
  public class ReportesController : Controller
  {
    [HttpGet]
    public IActionResult Solicitud()
    {
      return View();
    }
  }
}
