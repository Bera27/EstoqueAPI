namespace EstoqueAPI.ViewModels.Enderecos
{
    public class GetEnderecoViewModel
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}