using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SisHair.FuncionarioContext.Domain.Entities;

namespace SisHair.FuncionarioContext.Infra.Data.Mappings
{
    public class CargoMapping : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(c => c.Descricao)                
                .HasColumnType("nvarchar(200)");

            //// FOREIGN KEY
            builder.HasMany(c => c.Funcionario)
                .WithOne(f => f.Cargo)
                .HasForeignKey(f => f.CargoId);

            //builder.HasOne(f => f.Cargo)
            //    .WithMany(cargo => cargo.Funcionario)
            //    .HasForeignKey(f => f.CargoId);
        }
    }
}
