using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.DataSet
{
    public class BancoDados
    {
        private static string connectionString;

        static BancoDados()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public SqlCommand ObterConexao()
        {
            SqlConnection conexao = new SqlConnection(connectionString);
            conexao.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = conexao;
            return command;
        }
    }
}