using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Context;
using TrabalhoTcc.Models;

namespace TrabalhoTcc.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Index
        public ActionResult Index()
        {
            ViewBag.AvaliacoesParaIndex = db.Avaliacoes.Where(c => c.AvaliacaoAprovadaParaIndex == true).ToList();
            return View();
        }

        [HttpGet]
        public ActionResult SolicitarAgendamento()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AreaDoCliente()
        {
            //Verifica se os campos estão preenchidos
            //Estando tudo OK, Chama View AreaDoCliente
            return View();
        }

        public ActionResult Cadastro()
        {            
            return View();
        }
    }
}