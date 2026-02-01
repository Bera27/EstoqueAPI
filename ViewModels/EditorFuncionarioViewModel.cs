using System.ComponentModel.DataAnnotations;

namespace EstoqueAPI.ViewModels
{
    public class EditorFuncionarioViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Cargo é obrigatório")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; }
    }
}