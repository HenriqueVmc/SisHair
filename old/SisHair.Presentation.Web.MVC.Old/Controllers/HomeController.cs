using SisHair.Presentation.Web.MVC.Old.Context;
using System.Web.Mvc;

namespace SisHair.Presentation.Web.MVC.Old.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Index
        public ActionResult Index()
        {
            //ViewBag.AvaliacoesParaIndex = db.Avaliacoes.Where(c => c.AvaliacaoAprovadaParaIndex == true).ToList();
            return View();
        }

        public ActionResult EmConstrucao()
        {         
            return View();
        }

    }
}