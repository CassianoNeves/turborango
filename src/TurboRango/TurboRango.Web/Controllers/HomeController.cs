using System.Web.Mvc;
using TurboRango.Web.Models;
using System.Linq;

namespace TurboRango.Web.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Restaurantes.ToList());
        }
    }
}