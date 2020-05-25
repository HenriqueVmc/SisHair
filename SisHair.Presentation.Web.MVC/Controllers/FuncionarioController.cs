using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SisHair.FuncionarioContext.Application.Commands;
using SisHair.FuncionarioContext.Application.Handlers;
using SisHair.FuncionarioContext.Application.Queries;
using SisHair.FuncionarioContext.Application.ViewModels;
using System.Threading.Tasks;

namespace SisHair.Presentation.Web.MVC.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioQueries query;
        private readonly IFuncionarioCommandHandler commandHandler;

        public FuncionarioController(IFuncionarioQueries query, IFuncionarioCommandHandler commandHandler)
        {
            this.query = query;
            this.commandHandler = commandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var funcionarios = await query.BuscarFuncionariosComCargoAsync();
            return View(funcionarios);
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.CargosDisponiveis = await CarregarCargosDisponiveis();
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(FuncionarioViewModel model)
        {
            var command = new CadastrarFuncionarioCommand(
                model.Nome,
                model.DataNascimento,
                model.Cpf,
                model.Celular,
                model.Telefone,
                model.Email,
                model.Cargo.Id
            );

            var result = commandHandler.Handle(command);

            //return View(result);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {
            var funcionario = await query.BuscarPorIdAsync(id);

            ViewBag.CargosDisponiveis = await CarregarCargosDisponiveis(itemSelected: funcionario.Cargo.Id);

            return View("Cadastrar", funcionario);
        }

        [HttpPut, HttpPost, HttpPatch]
        public IActionResult Atualizar(FuncionarioViewModel model)
        {
            var command = new AtualizarFuncionarioCommand(
                model.Id,
                model.Nome,
                model.DataNascimento,
                model.Cpf,
                model.Celular,
                model.Telefone,
                model.Email,
                model.Cargo.Id
            );

            var result = commandHandler.Handle(command);

            return RedirectToAction("Index");
            //return View(result);
        }

        public IActionResult Remover(int id)
        {
            var result = commandHandler.Handle(new RemoverFuncionarioCommand(id));

            return RedirectToAction("Index");
            //return View(result);
        }

        private async Task<SelectList> CarregarCargosDisponiveis(int? itemSelected = null) =>
            new SelectList(await query.BuscarCargosDisponiveisAsync(), "Id", "Nome", itemSelected);
    }
}