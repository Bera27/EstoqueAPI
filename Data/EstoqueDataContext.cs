using EstoqueAPI.Data.Mappings;
using EstoqueAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EstoqueAPI.Data
{
    public class EstoqueDataContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendaItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=EstoqueDb;User ID=sa;Password=1q2w3e4r@#$;Encrypt=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
            modelBuilder.ApplyConfiguration(new FuncionarioMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new VendaItensMap());
            modelBuilder.ApplyConfiguration(new VendaMap());
        }
    }
}