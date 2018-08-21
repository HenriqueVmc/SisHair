using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrabalhoTcc.DataSet;

namespace TrabalhoTcc.Models.Conta
{
    public class LoginFuncionario
    {
        [Key]
        public int Id { get; set; }

        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public int Id_Funcionario { get; set; }
        public Funcionario Funcionario { get; set; }

        public static LoginFuncionario ValidarUsuario(string login, string senha)
        {
            LoginFuncionario ret = null;

            using (var cmd = new BancoDados().ObterConexao())
            {
                cmd.CommandText = string.Format("SELECT id, usuario, senha FROM usuario_funcionarios WHERE usuario = '{0}' AND senha = '{1}'", login, senha);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ret = new LoginFuncionario
                    {
                        Id = (int)reader["id"],
                        Usuario = (string)reader["usuario"],
                        Senha = (string)reader["senha"]

                    };
                }
            }
            return ret;
        }
    }
}