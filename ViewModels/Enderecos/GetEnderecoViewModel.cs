namespace EstoqueAPI.ViewModels.Enderecos
{
    public class GetEnderecoViewModel
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = null!;
        public string Rua { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Complemento { get; set; } = null!;
    }
}