using EstoqueAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstoqueAPI.Data.Mappings
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DataVenda)
                .IsRequired()
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.VendaTotal)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.HasOne(v => v.Funcionario)
                .WithMany(f => f.Vendas)
                .HasForeignKey(i => i.IdFuncionario)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}