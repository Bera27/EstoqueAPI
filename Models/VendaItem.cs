namespace EstoqueAPI.Models
{
    public class VendaItem
    {
        public int Id { get; set; }
        public int IdVenda { get; set; }
        public Venda Venda { get; set; } = null!;
        public int IdProduto { get; set; }
        public Produto Produto { get; set; } = null!;
        
        public int Quantidade { get; set; }
        public decimal PrecoUN { get; set; }
    }
}