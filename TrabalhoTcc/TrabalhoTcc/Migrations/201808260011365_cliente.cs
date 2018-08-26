namespace TrabalhoTcc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cliente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClienteNs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Data_nascimento = c.DateTime(nullable: false),
                        Celular = c.String(),
                        Telefone = c.String(),
                        Email = c.String(),
                        Senha = c.String(nullable: false),
                        ConfirmarSenha = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoginClientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Usuario = c.String(),
                        Senha = c.String(),
                        Id_Cliente = c.Int(nullable: false),
                        Cliente_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id)
                .Index(t => t.Cliente_Id);
            
            AlterColumn("dbo.Funcionarios", "Telefone", c => c.String(maxLength: 15));
            AlterColumn("dbo.Funcionarios", "Celular", c => c.String(maxLength: 15));
            AlterColumn("dbo.Servicos", "Descricao", c => c.String());
            DropColumn("dbo.Funcionarios", "Descricao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Funcionarios", "Descricao", c => c.String());
            DropForeignKey("dbo.LoginClientes", "Cliente_Id", "dbo.Clientes");
            DropIndex("dbo.LoginClientes", new[] { "Cliente_Id" });
            AlterColumn("dbo.Servicos", "Descricao", c => c.String(nullable: false));
            AlterColumn("dbo.Funcionarios", "Celular", c => c.String());
            AlterColumn("dbo.Funcionarios", "Telefone", c => c.String());
            DropTable("dbo.LoginClientes");
            DropTable("dbo.ClienteNs");
        }
    }
}
