using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Models;
using TrabalhoTcc.Repositorio;

namespace TrabalhoTcc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store(Cliente cliente)
        {
            int identificador = new ClientesRepositorio().Cadastrar(cliente);
            ViewBag.Cliente = cliente;
            return View();
        }

    }
}