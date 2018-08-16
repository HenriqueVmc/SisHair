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
            comando.CommandText = "SELECT id, nome, data_nascimento, celular, telefone, email FROM clientes";
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
                    Telefone = linha[4].ToString()
                };
                clientes.Add(cliente);
            }
            return clientes;            
        }
        public int Cadastrar(Cliente cliente)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = @"INSERT INTO clientes (nome, data_nascimento, celular, telefone) OUTPUT INSERTED.ID VALUES
(@NOME,
@DATA_NASCIMENTO,
@CELULAR,
@TELEFONE)";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.Data_nascimento);
            comando.Parameters.AddWithValue("@CELULAR", cliente.Celular);
            comando.Parameters.AddWithValue("@TELEFONE", cliente.Telefone);
            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            return id;
        }        
        
    }
}