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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Entrar(string usuario, string senha)
        {
            //Verificar se é Cliente ou Funcionário
            //Consulta na tabela de Acesso se existe 
            //Se estiver Válido, Liberar acesso com respectivas peremissões
            //bool existe = new ClientesRepositorio().Entrar(usuario, senha);
            /*
             método entrar
             public bool Entrar(string usuario, string senha){
                SqlCommand comando = new BancoDados().ObterConexao();
                comando.CommandText = "SELECT id from CLIENTES WHERE usuario = @USUARIO AND senha = @senha";
                comando.Parameters.AddWithValue("@ID", id);
                return comando.ExecuteNonQuery() == 1;
             }
            */
            return View();
        }

        [HttpPost]
        public ActionResult Store(Cliente cliente)
        {
            //Se estiver Válido, cadastrar cliente
            int identificador = new ClientesRepositorio().Cadastrar(cliente);
            //Retornanr para Login
            return View();
        }

    }
}