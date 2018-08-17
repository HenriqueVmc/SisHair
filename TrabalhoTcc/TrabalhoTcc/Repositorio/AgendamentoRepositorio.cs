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
    public class AgendamentoRepositorio
    {
        public List<Agendamento> ObterTodos()
        {
            List<Agendamento> agendamentos = new List<Agendamento>();
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "SELECT id, hora_inicio, hora_final, data FROM agendamentos";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            foreach(DataRow linha in tabela.Rows)
            {
                Agendamento agendamento = new Agendamento()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Hora_inicio = Convert.ToDateTime(linha[1].ToString()),
                    Hora_final = Convert.ToDateTime(linha[2].ToString()),
                    Data = Convert.ToDateTime(linha[3].ToString())
                };
                agendamentos.Add(agendamento);
            }
            return agendamentos;
        }

        public int Cadastrar(Agendamento agendamento)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = @"INSERT INTO agendamentos (hora_inicio, hora_final, data) OUTPUT INSERTED.ID VALUES
(@HORA_INICIO,
@HORA_FINAL,
@DATA)";
            comando.Parameters.AddWithValue("@HORA_INICIO", agendamento.Hora_inicio);
            comando.Parameters.AddWithValue("@HORA_FINAL", agendamento.Hora_final);
            comando.Parameters.AddWithValue("@DATA", agendamento.Data);
            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            return id;
        }

        public bool Alterar(Agendamento agendamento)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = @"UPDATE agendamentos SET
hora_inicio = @HORA_INICIO,
hora_final = @HORA_FINAL,
data = @DATA
WHERE id = @ID";
            comando.Parameters.AddWithValue("@HORA_INICIO", agendamento.Hora_inicio);
            comando.Parameters.AddWithValue("@HORA_FINAL", agendamento.Hora_final);
            comando.Parameters.AddWithValue("@DATA", agendamento.Data);
            comando.Parameters.AddWithValue("@ID", agendamento.Id);
            return comando.ExecuteNonQuery() == 1;
        }

        public bool Excluir(int id)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "DELETE FROM agendamentos WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            return comando.ExecuteNonQuery() == 1;
        }

        public Agendamento ObterPeloId(int id)
        {
            Agendamento agendamento = null;
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = @"SELECT hora_inicio, hora_final, data FROM agendamentos WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            if (tabela.Rows.Count == 1)
            {
                agendamento = new Agendamento();
                agendamento.Id = id;
                agendamento.Hora_inicio = Convert.ToDateTime(tabela.Rows[0][1].ToString());
                agendamento.Hora_final = Convert.ToDateTime(tabela.Rows[0][2].ToString());
                agendamento.Data = Convert.ToDateTime(tabela.Rows[0][3].ToString());
            }
            return agendamento;
        }
    }
}