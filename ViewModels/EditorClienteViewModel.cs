using System.ComponentModel.DataAnnotations;

namespace EstoqueAPI.ViewModels
{
    public class EditorClienteViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "CPF é obrigatório")]            
        public string Cpf { get; set; } = null!;

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; } = null!;
        //public DateTime DataCadastro { get; set; }
    }
}