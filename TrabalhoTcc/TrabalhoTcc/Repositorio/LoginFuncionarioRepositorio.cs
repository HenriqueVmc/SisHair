using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrabalhoTcc.DataSet;
using TrabalhoTcc.Models;

namespace TrabalhoTcc.Repositorio
{
    public class LoginFuncionarioRepositorio
    {
        public int Cadastrar(LoginFuncionario loginFuncionario)
        {
            SqlCommand command = new BancoDados().ObterConexao();
            command.CommandText = "INSERT INTO funcionarios (nome, data_nascimento, cpf, ctps) VALUES (@NOME, @NASCIMENTO, @CPF, @CTPS)";
            command.Parameters.AddWithValue("@NOME", loginFuncionario.Nome);
            command.Parameters.AddWithValue("@NASCIMENTO", loginFuncionario.DataNascimento);
            command.Parameters.AddWithValue("@CPF", loginFuncionario.CPF);
            command.Parameters.AddWithValue("@CTPS", loginFuncionario.CTPS);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());

            return id;
            





        }
    }
}