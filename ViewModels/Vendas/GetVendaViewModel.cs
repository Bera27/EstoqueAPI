namespace EstoqueAPI.ViewModels.Vendas
{
    public class GetVendaViewModel
    {
        public int Id { get; set; }
        public string Funcionario { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal VendaTotal { get; set; }
    }
}