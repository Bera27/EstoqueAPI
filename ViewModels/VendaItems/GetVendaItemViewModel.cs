namespace EstoqueAPI.ViewModels.VendaItems
{
    public class GetVendaItemViewModel
    {   
        public int Id { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUN { get; set; }
    }
}