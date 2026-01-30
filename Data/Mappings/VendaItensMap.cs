using EstoqueAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueAPI.Data.Mappings
{
    public class VendaItensMap : IEntityTypeConfiguration<VendaItem>
    {
        public void Configure(EntityTypeBuilder<VendaItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.PrecoUN)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(x => x.Quantidade)
                .IsRequired()
                .HasColumnType("INT");

            builder.HasOne(v => v.Venda)
                .WithMany(i => i.Itens)
                .HasForeignKey(x => x.IdVenda)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Produto)
                .WithMany(i => i.ProdItens)
                .HasForeignKey(x => x.IdProduto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}