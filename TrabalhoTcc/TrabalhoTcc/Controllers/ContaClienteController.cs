using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Models;

namespace TrabalhoTcc.Controllers
{
    public class ContaClienteController : Controller
    {
        // GET: ContaCliente
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginCliente loginC, string returnUrl)
        {
            return null;
        }

        [HttpGet]
        public ActionResult CriarConta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CriarConta(Cliente cliente)
        {
            //Validar dados
            //Ver se já existe no banco
            //Cadastrar Cliente

            //Redirection Solicitações
            return View();
        }

    }
}