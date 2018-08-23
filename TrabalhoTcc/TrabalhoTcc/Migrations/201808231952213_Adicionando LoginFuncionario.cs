namespace TrabalhoTcc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoLoginFuncionario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginFuncionarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Usuario = c.String(),
                        Senha = c.String(),
                        Id_Funcionario = c.Int(nullable: false),
                        Funcionario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcionarios", t => t.Funcionario_Id)
                .Index(t => t.Funcionario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginFuncionarios", "Funcionario_Id", "dbo.Funcionarios");
            DropIndex("dbo.LoginFuncionarios", new[] { "Funcionario_Id" });
            DropTable("dbo.LoginFuncionarios");
        }
    }
}
