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
    public class CargoRepositorio
    {
        public List<Cargo> ObterTodos()
        {
            List<Cargo> cargos = new List<Cargo>();
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = "SELECT id, nome, descricao FROM cargos";
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            foreach (DataRow linha in tabela.Rows)
            {
                Cargo cargo = new Cargo()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Nome = linha[1].ToString(),
                    Descricao = linha[2].ToString()
                };
                cargos.Add(cargo);
            }
            return cargos;
        }

        public int Cadastrar(Cargo cargo)
        {
            SqlCommand comando = new BancoDados().ObterConexao();
            comando.CommandText = @"INSERT INTO cargos (nome, descricao) OUTPUT INSERTED.ID VALUES (@NOME, @DESCRICAO)";
            comando.Parameters.AddWithValue("@NOME", cargo.Nome);
            comando.Parameters.AddWithValue("@DESCRICAO", cargo.Descricao);
            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            return id;
        }


       
       
       
    }
}