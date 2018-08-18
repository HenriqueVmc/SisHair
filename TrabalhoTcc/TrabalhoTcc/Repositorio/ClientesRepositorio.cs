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
    public class ClientesRepositorio
    {
        public List<Cliente> ObterTodos()
        {
            List<Cliente> clientes = new List<Cliente>();
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "SELECT id, nome, data_nascimento, celular, telefone, email, login, senha FROM clientes";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Cliente cliente = new Cliente()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Nome = linha[1].ToString(),
                    Data_nascimento = Convert.ToDateTime(linha[2].ToString()),
                    Celular = linha[3].ToString(),
                    Telefone = linha[4].ToString(),
                    Email = linha[5].ToString(),
                    Senha = linha[6].ToString()
                };
                clientes.Add(cliente);
            }
            return clientes;            
        }
        public int Cadastrar(Cliente cliente)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = @"INSERT INTO clientes (nome, data_nascimento, celular, telefone, email, login, senha) OUTPUT INSERTED.ID VALUES
(@NOME,
@DATA_NASCIMENTO,
@CELULAR,
@TELEFONE,
@EMAIL,
@NOME,
@SENHA)";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.Data_nascimento);
            comando.Parameters.AddWithValue("@CELULAR", cliente.Celular);
            comando.Parameters.AddWithValue("@TELEFONE", cliente.Telefone);
            comando.Parameters.AddWithValue("@EMAIL", cliente.Email);
            comando.Parameters.AddWithValue("@LOGIN", cliente.Login);
            comando.Parameters.AddWithValue("@SENHA", cliente.Senha);
            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            return id;
        }            
        public bool Alterar(Cliente cliente)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = @"UPDATE alunos SET
nome = @NOME,
celular = @CELULAR,
telefone = @TELEFONE
WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CELULAR", cliente.Celular);
            comando.Parameters.AddWithValue("@TELEFONE", cliente.Telefone);
            comando.Parameters.AddWithValue("@ID", cliente.Id);
            return comando.ExecuteNonQuery() == 1;
        }
        public bool Excluir(int id)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "DELETE from CLIENTES WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            return comando.ExecuteNonQuery() == 1;
        }

        public Cliente ObterPeloId(int id)
        {
            Cliente cliente = null;
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "SELECT nome, data_nascimento celular, telefone  WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            if (tabela.Rows.Count == 1)
            {
                cliente = new Cliente();
                cliente.Id = id;
                cliente.Nome = tabela.Rows[0][0].ToString();
                cliente.Data_nascimento = Convert.ToDateTime(tabela.Rows[0][1].ToString());
                cliente.Celular = tabela.Rows[0][2].ToString();
                cliente.Telefone = tabela.Rows[0][3].ToString();
            }
            return cliente;
        }
    }
}