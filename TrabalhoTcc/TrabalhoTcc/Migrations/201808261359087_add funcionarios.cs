namespace TrabalhoTcc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfuncionarios : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Funcionarios", "Cargo_Id", "dbo.Cargoes");
            DropIndex("dbo.Funcionarios", new[] { "Cargo_Id" });
            RenameColumn(table: "dbo.Funcionarios", name: "Cargo_Id", newName: "CargoId");
            CreateTable(
                "dbo.EnderecoFuncionarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rua = c.String(),
                        Bairro = c.String(),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                        Cep = c.String(),
                        FuncionarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Funcionarios", t => t.FuncionarioId, cascadeDelete: true)
                .Index(t => t.FuncionarioId);
            
            AlterColumn("dbo.Funcionarios", "Telefone", c => c.String(nullable: true, maxLength: 15));
            AlterColumn("dbo.Funcionarios", "CargoId", c => c.Int(nullable: true));
            CreateIndex("dbo.Funcionarios", "CargoId");
            AddForeignKey("dbo.Funcionarios", "CargoId", "dbo.Cargoes", "Id", cascadeDelete: true);
            DropColumn("dbo.Funcionarios", "Id_Cargo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Funcionarios", "Id_Cargo", c => c.Int(nullable: false));
            DropForeignKey("dbo.Funcionarios", "CargoId", "dbo.Cargoes");
            DropForeignKey("dbo.EnderecoFuncionarios", "FuncionarioId", "dbo.Funcionarios");
            DropIndex("dbo.EnderecoFuncionarios", new[] { "FuncionarioId" });
            DropIndex("dbo.Funcionarios", new[] { "CargoId" });
            AlterColumn("dbo.Funcionarios", "CargoId", c => c.Int());
            AlterColumn("dbo.Funcionarios", "Telefone", c => c.String(maxLength: 15));
            DropTable("dbo.EnderecoFuncionarios");
            RenameColumn(table: "dbo.Funcionarios", name: "CargoId", newName: "Cargo_Id");
            CreateIndex("dbo.Funcionarios", "Cargo_Id");
            AddForeignKey("dbo.Funcionarios", "Cargo_Id", "dbo.Cargoes", "Id");
        }
    }
}
