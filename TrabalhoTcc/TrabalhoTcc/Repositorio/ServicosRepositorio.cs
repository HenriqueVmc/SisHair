using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrabalhoTcc.DataSet;
using TrabalhoTcc.Models;

namespace TrabalhoTcc.Repositorio
{
    public class ServicosRepositorio
    {
        public int Servicos(Servicos servicos)
        {


            SqlCommand command = new BancoDados().ObterConexao();
            command.CommandText = "INSERT INTO servicos (servico, valor, duracao, descricao) VALUES (@SERVICO, @VALOR, @DURACAO, @DESCRICAO)";
            command.Parameters.AddWithValue("@SERVICO", servicos.Servico);
            command.Parameters.AddWithValue("@VALOR", servicos.Valor);
            command.Parameters.AddWithValue("@DURACAO", servicos.Duracao);
            command.Parameters.AddWithValue("@DESCRICAO", servicos.Descricao);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());

            return id;
        }
    }
}