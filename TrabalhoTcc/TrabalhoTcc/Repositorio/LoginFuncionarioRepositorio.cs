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
    public class LoginFuncionarioRepositorio
    {
        public List<LoginFuncionario> ObterTodos()
        {
            List<LoginFuncionario> funcionarios = new List<LoginFuncionario>();
            SqlCommand command = new BancoDados().ObterConexao();
            command.CommandText = "SELECT funcionarios.nome, funcionarios.data_nascimento, funcionarios.cpf, funcionarios.telefone, funcionarios.celular, funcionarios.email, funcionarios.descricao, cargos.descricao, cargos.cargo, enderecos.rua, enderecos.bairro, enderecos.numero_casa, enderecos.complemento, enderecos.estado,enderecos.cidade, enderecos.cep FROM funcionarios join cargos on funcionarios.id = cargos.id join enderecos on funcionarios.id = enderecos.id";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach(DataRow linha in tabela.Rows)
            {
                LoginFuncionario funcionario = new LoginFuncionario()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Nome = linha[1].ToString(),
                    DataNascimento = Convert.ToDateTime(linha[2].ToString())


                };
            }
            return funcionarios;
        }
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