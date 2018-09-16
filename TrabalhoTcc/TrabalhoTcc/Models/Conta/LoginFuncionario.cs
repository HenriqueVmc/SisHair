using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrabalhoTcc.Context;
using TrabalhoTcc.Helpers;

namespace TrabalhoTcc.Models.Conta
{
    public class LoginFuncionario
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

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        [Display(Name = "Permissao:")]
        public virtual Permissoes Permissoes { get; set; }
        public int PermissoesId { get; set; }


        public static LoginFuncionario ValidarUsuario(string login, string senha)
        {
            DBContext db = new DBContext();
            LoginFuncionario ret = null;

            senha = CriptoHelper.HashMD5(senha);

            ret = db.LoginFuncionarios.Where(x => x.Usuario == login && x.Senha == senha).SingleOrDefault();

            return ret;
        }


        public static LoginFuncionario RecuperarPeloId(int id)
        {
            DBContext db = new DBContext();
            //LoginFuncionario ret = null;

            var ret = db.LoginFuncionarios.Where(l => l.Id == id).SingleOrDefault();

            LoginFuncionario login = new LoginFuncionario(){
                Id = ret.Id,
                Usuario = ret.Usuario,
                Senha = ret.Senha,
                PermissoesId = ret.PermissoesId,
                FuncionarioId = ret.FuncionarioId
            };

            return login;
        }

        public static LoginFuncionario RecuperarUsuario(string email, string cpf)
        {
            DBContext db = new DBContext();
            LoginFuncionario login = null;
            Funcionario funcionario = null;

            funcionario = db.Funcionarios.Where(x => x.Email == email && x.Cpf == cpf).SingleOrDefault();
            if (funcionario != null)
            {
                login = db.LoginFuncionarios.Where(x => x.FuncionarioId == funcionario.Id).SingleOrDefault();
            }

            return login;
        }
    }
}