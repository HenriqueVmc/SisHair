using SisHair.Presentation.Web.MVC.Old.Context;
using SisHair.Presentation.Web.MVC.Old.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core;
using System.Linq;

namespace SisHair.Presentation.Web.MVC.Old.Models.Conta
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

            senha = CriptoHelper.HashMD5(senha);
            try
            {
                ret = db.LoginClientes.Where(x => x.Usuario == login && x.Senha == senha).SingleOrDefault();
            }catch(EntityException e)
            {
                return new LoginCliente();
            }
            return ret;
        }

        public static bool Existe(LoginCliente login)
        {
            DBContext db = new DBContext();
            bool ret = false;

            try
            {
                ret = db.LoginClientes.Where(x => x.Usuario == login.Usuario).Any();
            }
            catch (EntityException e)
            {
                return ret;
            }
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
