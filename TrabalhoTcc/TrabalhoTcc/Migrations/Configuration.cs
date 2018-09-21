namespace TrabalhoTcc.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TrabalhoTcc.Models;
    using TrabalhoTcc.Models.Conta;

    internal sealed class Configuration : DbMigrationsConfiguration<TrabalhoTcc.Context.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TrabalhoTcc.Context.DBContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // --------------  AUTO INSERT NAS TABELAS ----------------- //
            context.Servicos.AddOrUpdate(x => x.Id,
                new Servico() { Id = 1, Nome = "Corte Masculino", Valor = 30, Duracao = 30 },
                new Servico() { Id = 2, Nome = "Corte Feminino", Valor = 60, Duracao = 50 },
                new Servico() { Id = 3, Nome = "Corte Infantil", Valor = 15, Duracao = 50 }
            );

            context.Cargos.AddOrUpdate(x => x.Id,
                new Cargo() { Id = 1, Nome = "Cabeleireiro" },
                new Cargo() { Id = 2, Nome = "Pedicure" },
                new Cargo() { Id = 3, Nome = "Manicure" }
            );

            context.Funcionarios.AddOrUpdate(x => x.Id,
                new Funcionario() { Id = 1, Nome = "Rosimeire de Oliveira", DataNascimento = DateTime.Now, Telefone = "(00)00000-0000", Cpf = "000.000.000-00", CargoId = 1 },
                new Funcionario() { Id = 2, Nome = "Cláudia Soares", DataNascimento = DateTime.Now, Telefone = "(11)11111-1111", Cpf = "111.1111.1111-111", CargoId = 1 }
            );

            context.permissoes.AddOrUpdate(x => x.Id,
                new Permissoes() { Id = 1, TipoPermissao = "Administrador"},
                new Permissoes() { Id = 2, TipoPermissao = "Funcionario" }
            );

            context.LoginFuncionarios.AddOrUpdate(x => x.Id,
                new LoginFuncionario() { Id = 1, Usuario = "adm", Senha = "b09c600fddc573f117449b3723f23d64", FuncionarioId = 1, PermissoesId = 1 },
                new LoginFuncionario() { Id = 2, Usuario = "fun", Senha = "77004ea213d5fc71acf74a8c9c6795fb", FuncionarioId = 2, PermissoesId = 2 }
            );



            context.Clientes.AddOrUpdate(x => x.Id,
                new Cliente() { Id = 1, Nome = "Maria das Flores", Data_nascimento = DateTime.Now, Celular = "(47)99816-1423", Telefone = "(47)3441-2988", Email = "asdnjsadjsdh@gmail.com" }
                );

            context.Agendamentos.AddOrUpdate(x=> x.Id,
                new Agendamento() {Id = 1, DataHoraInicio = new DateTime(2018, 1, 18), DataHoraFinal = new DateTime(2018, 1, 18) , Situacao = "Confirmado", Descricao = "", Servicos= "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true},
                new Agendamento() { Id = 2, DataHoraInicio = new DateTime(2018, 1, 19), DataHoraFinal = new DateTime(2018, 1, 19), Situacao = "Confirmado", Descricao = "", Servicos = "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true },


                new Agendamento() {Id = 3, DataHoraInicio = new DateTime(2018, 2, 21), DataHoraFinal = new DateTime(2018, 2, 21), Situacao = "Confirmado", Descricao = "", Servicos = "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true },


                new Agendamento() {Id = 4, DataHoraInicio = new DateTime(2018, 3, 23), DataHoraFinal = new DateTime(2018, 3, 23), Situacao = "Confirmado", Descricao = "", Servicos = "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true },
                new Agendamento() { Id = 5, DataHoraInicio = new DateTime(2018, 3, 01), DataHoraFinal = new DateTime(2018, 3, 01), Situacao = "Confirmado", Descricao = "", Servicos = "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true },
                new Agendamento() { Id = 6, DataHoraInicio = new DateTime(2018, 3, 17), DataHoraFinal = new DateTime(2018, 3, 17), Situacao = "Confirmado", Descricao = "", Servicos = "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true },

                new Agendamento() { Id = 7, DataHoraInicio = new DateTime(2018, 5, 04), DataHoraFinal = new DateTime(2018, 5, 04), Situacao = "Confirmado", Descricao = "", Servicos = "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true },
                new Agendamento() { Id = 8, DataHoraInicio = new DateTime(2018, 5, 03), DataHoraFinal = new DateTime(2018, 5, 03), Situacao = "Confirmado", Descricao = "", Servicos = "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true },


                new Agendamento() {Id = 9, DataHoraInicio = new DateTime(2018, 6, 10), DataHoraFinal = new DateTime(2018, 6, 10) , Situacao = "Confirmado", Descricao = "", Servicos= "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true},
                new Agendamento() {Id = 10, DataHoraInicio = new DateTime(2018, 7, 12), DataHoraFinal = new DateTime(2018, 7, 12) , Situacao = "Confirmado", Descricao = "", Servicos= "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true},
                new Agendamento() {Id = 11, DataHoraInicio = new DateTime(2018, 9, 15), DataHoraFinal = new DateTime(2018, 9, 15) , Situacao = "Confirmado", Descricao = "", Servicos= "Corte Masculino", FuncionarioId = 1, ClienteId = 1, RegistroAgendamentoAtivo = true}
                );
        }
    }
}
