using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrabalhoTcc.DataSet;
using TrabalhoTcc.Models;

namespace TrabalhoTcc.Repositorio
{
	public class SolicitacaoRepositorio
	{
        public List<Solicitacoes> ObterTodos()
        {
            List<Solicitacoes> solicitacoes = new List<Solicitacoes>();
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "SELECT id, hora_inicio, data, situacao FROM solicitacoes";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            foreach(DataRow linha in tabela.Rows)
            {
                Solicitacoes solicitacao = new Solicitacoes()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Hora_inicio = Convert.ToDateTime(linha[1].ToString()),
                    Situacao = linha[3].ToString()
                };
                solicitacoes.Add(solicitacao);
            }
            return solicitacoes;
        }

        public int Cadastrar(Solicitacoes solicitacao)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = @"INSERT INTO solicitacoes (hora_inicio, data, situacao) OUTPUT INSERTED.ID VALUES
(@HORA_INICIO,
@DATA,
@SITUACAO)";
            comando.Parameters.AddWithValue("@HORA_INICIO", solicitacao.Hora_inicio);
            comando.Parameters.AddWithValue("@DATA", solicitacao.Data);
            comando.Parameters.AddWithValue("@SITUACAO", solicitacao.Situacao);
            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(Solicitacoes solicitacao)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = @"UPDATE solicitacoes SET
hora_inicio = @HORA_INICIO,
data = @DATA,
situacao = @SITUACAO
WHERE id = @ID";
            comando.Parameters.AddWithValue("@HORA_INICIO", solicitacao.Hora_inicio);
            comando.Parameters.AddWithValue("@DATA", solicitacao.Data);
            comando.Parameters.AddWithValue("@SITUACAO", solicitacao.Situacao);
            comando.Parameters.AddWithValue("@ID", solicitacao.Id);
            return comando.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "DELETE FROM agendamentos WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            return comando.ExecuteNonQuery() == 1;
        }
        
        public Solicitacoes ObterPeloId(int id)
        {
            Solicitacoes solicitacao = null;
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "SELECT hora_inicio, data, situacao FROM solicitacoes WHERE id = @ID"
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            if (tabela.Rows.Count == 1)
            {
                solicitacao = new Solicitacoes();
                solicitacao.Id = id;
                solicitacao.Hora_inicio = Convert.ToDateTime(tabela.Rows[0][0].ToString());
                solicitacao.Situacao = tabela.Rows[0][1].ToString();                
            }
            return solicitacao;

            
        }                   
	}
}