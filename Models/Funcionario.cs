namespace EstoqueAPI.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public string Telefone { get; set; } = null!;

        public ICollection<Venda> Vendas { get; set; } = [];
    }
}