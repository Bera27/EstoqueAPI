using EstoqueAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueAPI.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Quantidade)
                .IsRequired()
                .HasColumnType("INT");

            builder.Property(x => x.PrecoCompra)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.PrecoVenda)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.CompradoEm)
                .IsRequired()
                .HasColumnType("DATETIME");
        }
    }
}