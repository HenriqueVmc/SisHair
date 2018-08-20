using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrabalhoTcc.Controllers
{
    public class FunciController : Controller
    {
        // GET: Funci
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgendamentoDeServico()
        {
            return View();
        }
    }
}