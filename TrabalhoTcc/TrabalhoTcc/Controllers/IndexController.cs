using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrabalhoTcc.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AreaDoCliente()
        {            
            return View();
        }

        public ActionResult Cadastro()
        {
            //Estando tudo OK, Chama View AreaDoCliente
            return View();
        }
    }
}