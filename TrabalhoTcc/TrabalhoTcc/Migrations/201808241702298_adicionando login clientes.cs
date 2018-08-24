namespace TrabalhoTcc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionandologinclientes : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginClientes", "Cliente_Id", "dbo.Clientes");
            DropIndex("dbo.LoginClientes", new[] { "Cliente_Id" });
            DropTable("dbo.LoginClientes");
        }
    }
}
