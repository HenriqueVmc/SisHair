using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrabalhoTcc.Models.Conta
{
    public class LoginFuncionario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o usuário")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        public static LoginFuncionario ValidarUsuario(string login, string senha)
        {
           LoginFuncionario ret = null;
           using (var db = new SqlConnection())
           {
               //    ret = db.Usuarios
               //        .Include(x => x.Perfis)
               //        .Where(x => x.Usuario == login && x.Senha == senha)
               //        .SingleOrDefault();

               db.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Tcc;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
               db.Open();
               using (var cmd = new SqlCommand())
               {
                   cmd.Connection = db;
                   cmd.CommandText = string.Format("SELECT Id, Usuario, Senha FROM LoginFuncionarios WHERE Usuario = '{0}' AND Senha = '{1}'", login, senha);
                   var reader = cmd.ExecuteReader();

                   if (reader.Read())//Encontrou
                   {
                       ret = new LoginFuncionario
                       {
                           Id = (int)reader["Id"],
                           Usuario = (string)reader["Usuario"],
                           Senha = (string)reader["Senha"]
                       };
                   }

               }
               return ret;
           }
        }
    }
}