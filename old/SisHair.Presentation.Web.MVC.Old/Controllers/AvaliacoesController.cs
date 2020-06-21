using SisHair.Presentation.Web.MVC.Old.Context;
using System.Web.Mvc;

namespace SisHair.Presentation.Web.MVC.Old.Controllers
{
    public class AvaliacoesController : Controller
    {
        private DBContext db = new DBContext();
        public ActionResult Index()
        {
            //var result = db.Avaliacoes.Include(a => a.Agendamento.Cliente).ToList();


            //ViewBag.Avaliacoes = result;


            //return View();
            return RedirectToAction("EmConstrucao", "Home");
        }
    }


}
