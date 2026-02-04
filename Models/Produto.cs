namespace EstoqueAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public int Quantidade { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
        public DateTime CompradoEm { get; set; }

        public ICollection<VendaItem> ProdItens { get; set; } = [];
    }
}