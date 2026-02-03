using System.ComponentModel.DataAnnotations;

namespace EstoqueAPI.ViewModels.VendaItems
{
    public class PostVendaItemViewModel
    {
        [Required(ErrorMessage = "O Id do produto é obrigatorio")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O Id da venda é obrigatorio")]
        public int IdVenda { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatorio")]
        public int Quantidade { get; set; }

        [Required (ErrorMessage = "O preço é obrigatorio")]
        public decimal PrecoUN { get; set; }
    }
}