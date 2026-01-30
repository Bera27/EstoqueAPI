namespace EstoqueAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal PrecoVenda { get; set; }
        public DateTime CompradoEm { get; set; }

        public ICollection<VendaItem> ProdItens { get; set; } = new List<VendaItem>();
    }
}