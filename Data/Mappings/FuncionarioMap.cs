using EstoqueAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueAPI.Data.Mappings
{
    public class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Senha)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.Telefone)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(13);

            builder.Property(x => x.Cargo)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}