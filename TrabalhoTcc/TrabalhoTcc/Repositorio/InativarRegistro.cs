using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TrabalhoTcc.Context;
using TrabalhoTcc.Models;

namespace TrabalhoTcc.Repositorio
{
    public class InativarRegistro
    {

        private DBContext db = new DBContext();
        //Inativar Cargo
        public bool InativarRegistroCargo(Cargo cargo)
        {            
            //SqlCommand comando = new BancoDados().ObterConexao();
            //comando.CommandText = "UPDATE cargos SET RegistroCargoAtivo = @REGISTROCARGOATIVO where id = @ID";
            //comando.Parameters.AddWithValue("@REGISTROCARGOATIVO", cargo.RegistroCargoAtivo);
            //return comando.ExecuteNonQuery() == 1;            
            //cargo.RegistroCargoAtivo = false;
            var Resultado = db.Cargos.Where(c => c.Id == cargo.Id).SingleOrDefault();
            db.Entry(Resultado).State = EntityState.Modified;
            int a = db.SaveChanges();
            //return (a == 1) ? true : false;
            if (a == 1)
            {
               return true;
            }
            else
            {
                return false;
            }

        }        
    }
}