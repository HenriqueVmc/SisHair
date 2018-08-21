using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TrabalhoTcc.DataSet;

namespace TrabalhoTcc.Models.Conta
{
    public class LoginCliente
    {
        [Key]
        public int Id { get; set; }

        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public int Id_Cliente { get; set; }
        public Cliente Cliente { get;set; }

        public static LoginCliente ValidarUsuario(string login, string senha)
        {
            LoginCliente ret = null;

            using (var cmd = new BancoDados().ObterConexao())
            {
                cmd.CommandText = string.Format("SELECT id, usuario, senha FROM usuario_clientes WHERE usuario = '{0}' AND senha = '{1}'", login, senha);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ret = new LoginCliente
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