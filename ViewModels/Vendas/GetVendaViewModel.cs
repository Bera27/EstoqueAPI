namespace EstoqueAPI.ViewModels.Vendas
{
    public class GetVendaViewModel
    {
        public int Id { get; set; }
        public string Funcionario { get; set; } = null!;
        public DateTime DataVenda { get; set; }
        public decimal VendaTotal { get; set; }
    }
}