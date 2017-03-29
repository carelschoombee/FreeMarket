using System.Web.Mvc;

namespace FreeMarket.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Ellumin8")]
    public class Ellumin8Controller : Controller
    {
        // GET: Ellumin8
        public ActionResult Index()
        {
            return View();
        }
    }
}