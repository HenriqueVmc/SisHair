using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabalhoTcc.Context;
using TrabalhoTcc.Models;
using Newtonsoft.Json;
using TrabalhoTcc.Models.Conta;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Net.Mail;

namespace TrabalhoTcc.Controllers
{
    public class AvaliacoesController : Controller
    {
        private DBContext db = new DBContext();
        public ActionResult Index()
        {
            //var result = db.Avaliacoes.Include(a => a.Agendamento.Cliente).ToList();


            //ViewBag.Avaliacoes = result;


            //return View();
            return RedirectToAction("EmConstrucao", "Home");
        }
    }


}
