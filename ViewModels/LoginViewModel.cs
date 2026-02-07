using System.ComponentModel.DataAnnotations;

namespace EstoqueAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Informe a senha")]
        public string Senha { get; set; } = null!;
    }
}