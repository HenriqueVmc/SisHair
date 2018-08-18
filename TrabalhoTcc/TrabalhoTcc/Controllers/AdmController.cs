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
            List<Cliente> clientes = new ClientesRepositorio().ObterTodos();
            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult CadastrarFuncionario()
        {
            List<Cargo> cargo = new CargoRepositorio().ObterTodos();
            ViewBag.Cargo = cargo;
            return View();
        }

        [HttpPost]
        public ActionResult Store(Cliente cliente)
        {
            int identificador = new ClientesRepositorio().Cadastrar(cliente);
            ViewBag.Cliente = cliente;
            return View("ConsultarCliente");
        }


/////////////////////////////////////////////////////////////////////////////
///////////  CARGOS ////////////////  CARGOS   /////////////// CARGOS ///////


        public ActionResult CadastrarCargo()
        {

            return View();
        }

        [HttpPost]
        public ActionResult StoreCargo(Cargo cargo)
        {
            int identificador = new CargoRepositorio().Cadastrar(cargo);
            ViewBag.Cargo = cargo;
            return View();
        }

        [HttpGet]
        public ActionResult ConsultarCargo()
        {
            List<Cargo> cargos = new CargoRepositorio().ObterTodos();
            ViewBag.Cargo = cargos;
            return View();
        }
    }
}