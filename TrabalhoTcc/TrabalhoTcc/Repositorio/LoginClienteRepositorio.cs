using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrabalhoTcc.DataSet;
using TrabalhoTcc.Models;

namespace TrabalhoTcc.Repositorio
{
    public class LoginClienteRepositorio
    {
        public int Cadastrar(LoginCliente logincliente)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "@INSERT INTO clientes (nome, data_nascimento, email, celular, telefone, login, senha) OUTPUT INSERTED.ID VALUES (@NOME, @DATA_NASCIMENTO, @EMAIL, @CELULAR, @TELEFONE, @LOGIN, @SENHA)";
            comando.Parameters.AddWithValue("@NOME",logincliente.Nome );
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", logincliente.DataNascimento);
            comando.Parameters.AddWithValue("@EMAIL", logincliente.Email);
            comando.Parameters.AddWithValue("@CELULAR", logincliente.Celular);
            comando.Parameters.AddWithValue("@TELEFONE", logincliente.Telefone);
            comando.Parameters.AddWithValue("@LOGIN", logincliente.Login);
            comando.Parameters.AddWithValue("@SENHA", logincliente.Senha);
            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            return id;


        }
    }
}