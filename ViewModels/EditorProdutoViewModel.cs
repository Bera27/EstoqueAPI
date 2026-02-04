using System.ComponentModel.DataAnnotations;

namespace EstoqueAPI.ViewModels
{
    public class EditorProdutoViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "A descrição é obrigatório")]
        public string Descricao { get; set; } = null!;

        [Required(ErrorMessage = "A quantidade é obrigatório")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço de compra é obrigatório")]
        public decimal PrecoCompra { get; set; }

        [Required(ErrorMessage = "O preço de venda é obrigatório")]
        public decimal PrecoVenda { get; set; }

        [Required(ErrorMessage = "A data da compra é obrigatório")]
        public DateTime CompradoEm { get; set; }
    }
}