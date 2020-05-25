using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisHair.FuncionarioContext.Domain.Entities;

namespace SisHair.FuncionarioContext.Infra.Data.Mappings
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasColumnType("nvarchar(100)");
            
            builder.Property(f => f.DataNascimento)
                .IsRequired()
                .HasColumnType("datetime");
            
            builder.Property(f => f.Cpf)
                .IsRequired()
                .HasColumnType("nvarchar(20)");
            
            builder.Property(f => f.RegistroFuncionarioAtivo)
                .IsRequired()
                .HasColumnType("bit");

            //// FOREIGN KEY
            //builder.HasOne(f => f.Cargo)
            //    .WithMany(cargo => cargo.Funcionario)
            //    .HasForeignKey(f => f.CargoId);

            builder.OwnsOne(f => f.Contato, c =>
            {
                c.Property(c => c.Celular)
                    .HasColumnType("nvarchar(15)");
                
                c.Property(c => c.Telefone)
                    .HasColumnType("nvarchar(15)");

                c.Property(c => c.Email)
                    .HasColumnType("nvarchar(100)");
            });
        }
    }
}
