using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Models;
using TrabalhoTcc.Models.Conta;

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
            var usuario = LoginCliente.ValidarUsuario(loginC.Usuario, loginC.Senha);

            if (usuario != null)
            {
                return RedirectToAction("SolicitarAgendamento", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login Inválido");
            }

            return View(loginC);
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