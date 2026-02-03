using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueAPI.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int IdFuncionario { get; set; }
        public Funcionario Funcionario { get; set; }

        public DateTime DataVenda { get; set; }
        public decimal VendaTotal { get; set; }

        public ICollection<VendaItem> Itens { get; set; } = new List<VendaItem>();
    }
}