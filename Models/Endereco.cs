namespace EstoqueAPI.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; } = null!;
        
        public string Rua { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Complemento { get; set; } = null!;
        
    }
}