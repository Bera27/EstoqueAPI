namespace EstoqueAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public DateTime DataCadastro { get; set; }

        public ICollection<Endereco> Enderecos { get; set; } = [];

    }
}