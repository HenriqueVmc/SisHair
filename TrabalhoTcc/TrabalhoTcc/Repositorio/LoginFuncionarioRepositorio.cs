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
            command.CommandText = @"SELECT funcionarios.nome, funcionarios.data_nascimento, funcionarios.cpf, funcionarios.telefone, funcionarios.celular, funcionarios.email, funcionarios.descricao, cargos.cargo, cargos.descricao, enderecos.rua, enderecos.bairro, enderecos.numero_casa, enderecos.complemento, enderecos.estado,enderecos.cidade, enderecos.cep FROM funcionarios 
              join cargos on (funcionarios.id_cargo = cargos.id) 
              join enderecos on (funcionarios.id_endreco = enderecos.id)";
            DataTable tabela = new DataTable();
            tabela.Load(command.ExecuteReader());
            foreach(DataRow linha in tabela.Rows)
            {
                LoginFuncionario funcionario = new LoginFuncionario()
                {
                    Id = Convert.ToInt32(linha[0].ToString()),
                    Nome = linha[1].ToString(),
                    DataNascimento = Convert.ToDateTime(linha[2].ToString()),
                    CPF = linha[3].ToString(),
                    Telefone = linha[4].ToString(),
                    Celular = linha[5].ToString(),
                    Email = linha[6].ToString(),
                    Descricao = linha[7].ToString(),

                };


                Cargo cargo = new Cargo()
                {
                    Id = Convert.ToInt32(linha[8].ToString()),
                    Cargo = linha[9].ToString(),
                    Descricao = linha[10].ToString()
                };
                funcionario.Cargo = cargo;

                Endereco endereco = new Endereco()
                {
                    Rua = linha[11].ToString(),
                    Bairro = linha[12].ToString(),
                    NumeroDaCasa = linha[13].ToString(),
                    complemento = linha[14].ToString(),
                    Estado = linha[15].ToString(),
                    Cidade = linha[16].ToString(),
                    CEP = linha[17].ToString()
                };
                funcionario.Endereco = endereco;
            }
            return funcionarios;
        }
        public int Cadastrar(LoginFuncionario loginFuncionario)
        {
            SqlCommand command = new BancoDados().ObterConexao();
            command.CommandText = "INSERT INTO funcionarios (nome, data_nascimento, cpf, telefone, celular, email, descricao, id_cargo, id_endereco) VALUES (@NOME, @NASCIMENTO, @CPF, @TELEFONE, @CELULAR, @EMAIL, @DESCRICAO, @ID_CARGO, @ID_ENDERECO)";
            command.Parameters.AddWithValue("@NOME", loginFuncionario.Nome);
            command.Parameters.AddWithValue("@NASCIMENTO", loginFuncionario.DataNascimento);
            command.Parameters.AddWithValue("@CPF", loginFuncionario.CPF);
            command.Parameters.AddWithValue("@TELEFONE",loginFuncionario.Telefone);
            command.Parameters.AddWithValue("@CELULAR",loginFuncionario.Celular);
            command.Parameters.AddWithValue("@EMAIL",loginFuncionario.Email);
            command.Parameters.AddWithValue("@DESCRICAO", loginFuncionario.Descricao);
            command.Parameters.AddWithValue("@ID_CARGO", loginFuncionario.Id_Cargo);
            command.Parameters.AddWithValue("@ID_ENDERECO", loginFuncionario.Id_Endereco);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());

            return id;
        }
    }
}