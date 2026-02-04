using System.ComponentModel.DataAnnotations;
using EstoqueAPI.Models;

namespace EstoqueAPI.ViewModels
{
    public class PostEnderecoViewModel
    {
        [Required(ErrorMessage = "Id do cliente é obrigatório")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Rua é obrigatório")]
        public string Rua { get; set; } = null!;

        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string Bairro { get; set; } = null!;

        [Required(ErrorMessage = "Numero é obrigatório")]
        public string Numero { get; set; } = null!;

        public string Complemento { get; set; } = null!;
    }
}