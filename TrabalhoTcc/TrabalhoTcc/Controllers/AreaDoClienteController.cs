using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Context;

namespace TrabalhoTcc.Controllers
{
    public class AreaDoClienteController : Controller
    {
        // GET: AreaDoCliente
        private DBContext db = new DBContext();

        [HttpGet]
        public ActionResult Index(int? ClienteId)
        {
            ViewBag.ClienteId = ClienteId;
            ViewBag.AgendamentosParaAvaliar = db.Agendamentos.Where(c => c.Id == ClienteId).ToList();
            return View();
        }
        public ActionResult AvaliarAgendamento()
        {
            return null;
        }


    }
}