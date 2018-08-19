using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Models;

namespace TrabalhoTcc.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AreaDoCliente(Cliente cliente)
        {
            //Verifica se os campos estão preenchidos
            //Estando tudo OK, Chama View AreaDoCliente
            ViewBag.Cliente = cliente;
            return View();
        }

        public ActionResult Cadastro()
        {            
            return View();
        }
    }
}