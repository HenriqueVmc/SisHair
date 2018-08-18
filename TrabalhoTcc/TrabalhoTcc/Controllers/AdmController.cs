using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Models;
using TrabalhoTcc.Repositorio;

namespace TrabalhoTcc.Controllers
{
    public class AdmController : Controller
    {
        // GET: Adm
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConsultarCliente()
        {
            return View();
        }

        public ActionResult CadastrarFuncionario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store(Cliente cliente)
        {
            int identificador = new ClientesRepositorio().Cadastrar(cliente);
            ViewBag.Cliente = cliente;
            return View("ConsultarCliente");
        }
    }
}