using EstoqueAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueAPI.Data.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(11);

            builder.Property(x => x.Telefone)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(13);

            builder.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}