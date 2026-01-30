using EstoqueAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueAPI.Data.Mappings
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Rua)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Bairro)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Numero)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(10);

            builder.Property(x => x.Complemento)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.Cliente)
                .WithMany(c => c.Enderecos)
                .HasForeignKey(e => e.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}