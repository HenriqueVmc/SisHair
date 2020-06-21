using SisHair.Presentation.Web.MVC.Old.Context;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SisHair.Presentation.Web.MVC.Old.Models
{
    public class ServicosSolicitacao
    {
        private DBContext db = new DBContext();

        [Key]
        public int Id { get; set; }

        public virtual Solicitacao Solicitacao { get; set; }
        public int SolicitacaoId { get; set; }

        public virtual Servico Servico { get; set; }
        public int ServicoId { get; set; }

        public string salvarServicosSolicitacao(Solicitacao a, List<int> servicos)
        {
            foreach (int idServico in servicos)
            {
                var servico = db.Servicos.Where(s => s.Id == idServico).SingleOrDefault();

                ServicosSolicitacao sa = new ServicosSolicitacao();
                sa.SolicitacaoId = a.Id;
                sa.ServicoId = servico.Id;

                db.ServicosSolicitacao.Add(sa);

                a.Descricao += (string.IsNullOrEmpty(a.Descricao)) ? sa.Servico.Nome : ", " + sa.Servico.Nome;
                db.SaveChanges();

            }
            return a.Descricao;
        }
    }
}