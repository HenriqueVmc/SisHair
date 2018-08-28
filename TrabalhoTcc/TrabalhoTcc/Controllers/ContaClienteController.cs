using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Context;
using TrabalhoTcc.Models;
using TrabalhoTcc.Models.Conta;

namespace TrabalhoTcc.Controllers
{
    public class ContaClienteController : Controller
    {
        private DBContext db = new DBContext();

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
            if (!ModelState.IsValid)
            {
                return View(loginC);
            }
            var usuario = LoginCliente.ValidarUsuario(loginC.Usuario, loginC.Senha); 

            if (usuario != null)
            {
                return RedirectToAction("Agendamento", "Solicitacao", new {@idCliente = usuario.ClienteId});
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
        public ActionResult CriarConta(Cliente cliente, LoginCliente loginC)
        {
            if (ModelState.IsValid)
            {                         
                db.Clientes.Add(cliente);
                
                var LoginCliente = new LoginCliente()
                {
                    Usuario = loginC.Usuario,
                    Senha = loginC.Senha,
                    ClienteId = cliente.Id
                };

                db.LoginClientes.Add(LoginCliente);

                db.SaveChanges();
                return RedirectToAction("Login", "ContaCliente");
            }


            ViewBag.cliente = cliente;
            return View();
        }

    }
}