namespace TrabalhoTcc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cellnull : DbMigration
    {
        public override void Up()
        {
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
        }
    }
}