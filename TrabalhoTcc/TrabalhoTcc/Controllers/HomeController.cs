using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Models;

namespace TrabalhoTcc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
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