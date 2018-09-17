using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Context;

namespace TrabalhoTcc.Controllers
{
    public class EstatisticasController : Controller
    {
        private DBContext db = new DBContext();
        // GET: Estatisticas
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Funcionario")]
        public ActionResult Agendamentos()
        {

            return View();
        }

        [HttpGet]
        public JsonResult GetAgendamentosByMes()
        {
            //SELECT MONTH(DataHoraFinal), COUNT(DataHoraFinal) FROM Agendamentoes WHERE YEAR(DataHoraFinal) = '2018' GROUP BY  MONTH(DataHoraFinal);
            var x = from ag in db.Agendamentos
                    where ag.DataHoraFinal.Year == DateTime.Now.Year
                    group ag by ag.DataHoraFinal.Month into groupmonth
                    select new
                    {
                        Quantidade = groupmonth.Count(),
                        Mes = groupmonth.Key
                    };
            var registros = x.ToList();

            var valores = new int[12];
            foreach (var y in registros)
            {
                valores[(y.Mes) - 1] = y.Quantidade;
            }
            //var agendamentosbyMes = db.Agendamentos.Where(a => a.DataHoraFinal.Year == DateTime.Now.Year).GroupBy(a => a.DataHoraFinal.Month).Count();            

            return new JsonResult { Data = valores, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult Funcionarios()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAgendamentosByMesFuncionarios()
        {
            //SELECT COUNT(a.DataHoraFinal) FROM Agendamentoes a  WHERE YEAR(a.DataHoraFinal) = 2018 GROUP BY a.FuncionarioId; 
            var x = from ag in db.Agendamentos                    
                    where ag.DataHoraFinal.Year == DateTime.Now.Year                   
                    group ag by ag.FuncionarioId into groupFunc
                    select new
                    {
                        Quantidade =  groupFunc.Count()
                    };
            var registros = x.ToList();

            var valores = new int[registros.Count];
            int i = 0;
            foreach (var y in registros)
            {
                valores[i] = y.Quantidade;
                i++;
            }
            //var agendamentosbyMes = db.Agendamentos.Where(a => a.DataHoraFinal.Year == DateTime.Now.Year).GroupBy(a => a.DataHoraFinal.Month).Count();            

            return new JsonResult { Data = valores, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public JsonResult GetFuncionarios()
        {
            //SELECT COUNT(a.DataHoraFinal) FROM Agendamentoes a  WHERE YEAR(a.DataHoraFinal) = 2018 GROUP BY a.FuncionarioId; 
            var x = from ag in db.Agendamentos
                    where ag.DataHoraFinal.Year == DateTime.Now.Year
                    group ag by ag.Funcionario.Nome into groupFunc
                    select new
                    {
                        Nome = groupFunc.Key
                    };
            var registros = x.ToList();

            var valores = new string[registros.Count];
            int i = 0;
            foreach (var y in registros)
            {
                valores[i] = y.Nome;
                i++;
            }
            //var agendamentosbyMes = db.Agendamentos.Where(a => a.DataHoraFinal.Year == DateTime.Now.Year).GroupBy(a => a.DataHoraFinal.Month).Count();            

            return new JsonResult { Data = valores, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}