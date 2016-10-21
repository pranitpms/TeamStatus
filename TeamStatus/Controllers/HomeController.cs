using System.Web.Mvc;

namespace TeamStatus.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["CurrentPage"] = "Home";
            return View();
        }

    }
}
