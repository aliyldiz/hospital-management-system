using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
