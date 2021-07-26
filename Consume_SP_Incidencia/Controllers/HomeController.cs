using System.Data;
using System.Web.Mvc;

namespace Consume_SP_Incidencia.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
