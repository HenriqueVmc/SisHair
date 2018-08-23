namespace TrabalhoTcc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        DataNascimento = c.DateTime(nullable: false),
                        Cpf = c.String(nullable: false),
                        Telefone = c.String(),
                        Celular = c.String(),
                        Descricao = c.String(),
                        Email = c.String(),
                        Id_Cargo = c.Int(nullable: false),
                        Cargo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cargoes", t => t.Cargo_Id)
                .Index(t => t.Cargo_Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Data_nascimento = c.DateTime(nullable: false),
                        Celular = c.String(nullable: false, maxLength: 15),
                        Telefone = c.String(nullable: false, maxLength: 15),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Servicos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Servico = c.String(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Duracao = c.DateTime(nullable: false),
                        Descricao = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Agendamentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Hora_inicio = c.DateTime(nullable: false),
                        Hora_final = c.DateTime(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Cliente_Id = c.Int(),
                        Funcionario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id)
                .ForeignKey("dbo.Funcionarios", t => t.Funcionario_Id)
                .Index(t => t.Cliente_Id)
                .Index(t => t.Funcionario_Id);
            
            CreateTable(
                "dbo.AgendamentoServicos",
                c => new
                    {
                        Agendamento_Id = c.Int(nullable: false),
                        Servicos_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Agendamento_Id, t.Servicos_Id })
                .ForeignKey("dbo.Agendamentoes", t => t.Agendamento_Id, cascadeDelete: true)
                .ForeignKey("dbo.Servicos", t => t.Servicos_Id, cascadeDelete: true)
                .Index(t => t.Agendamento_Id)
                .Index(t => t.Servicos_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AgendamentoServicos", "Servicos_Id", "dbo.Servicos");
            DropForeignKey("dbo.AgendamentoServicos", "Agendamento_Id", "dbo.Agendamentoes");
            DropForeignKey("dbo.Agendamentoes", "Funcionario_Id", "dbo.Funcionarios");
            DropForeignKey("dbo.Agendamentoes", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.Funcionarios", "Cargo_Id", "dbo.Cargoes");
            DropIndex("dbo.AgendamentoServicos", new[] { "Servicos_Id" });
            DropIndex("dbo.AgendamentoServicos", new[] { "Agendamento_Id" });
            DropIndex("dbo.Agendamentoes", new[] { "Funcionario_Id" });
            DropIndex("dbo.Agendamentoes", new[] { "Cliente_Id" });
            DropIndex("dbo.Funcionarios", new[] { "Cargo_Id" });
            DropTable("dbo.AgendamentoServicos");
            DropTable("dbo.Agendamentoes");
            DropTable("dbo.Servicos");
            DropTable("dbo.Clientes");
            DropTable("dbo.Funcionarios");
            DropTable("dbo.Cargoes");
        }
    }
}
