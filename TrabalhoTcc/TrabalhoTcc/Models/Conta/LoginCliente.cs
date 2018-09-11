using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrabalhoTcc.Context;
using TrabalhoTcc.Helpers;

namespace TrabalhoTcc.Models.Conta
{
    public class LoginCliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuário:")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public static LoginCliente ValidarUsuario(string login, string senha)
        {
            DBContext db = new DBContext();
            LoginCliente ret = null;

            //senha = CriptoHelper.HashMD5(senha);
            ret = db.LoginClientes.Where(x => x.Usuario == login && x.Senha == senha).SingleOrDefault();

            return ret;
        }
    }
}

/*
 * 
 * using (var db = new SqlConnection())
            {
                db.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tcc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
                db.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = db;
                    cmd.CommandText = string.Format("SELECT id, usuario, senha, clienteId FROM loginclientes WHERE usuario = '{0}' AND senha = '{1}'", login, senha);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        ret = new LoginCliente
                        {
                            Id = (int)reader["id"],
                            Usuario = (string)reader["usuario"],
                            Senha = (string)reader["senha"],
                            ClienteId = (int)reader["clienteId"]
                        };
                    }
                }              
            }
*/
