using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueAPI.ViewModels.Vendas
{
    public class PostVendaViewModel
    {
        public int IdFuncionario { get; set; }
        public List<ItemPedidoInput> Itens { get; set; } = [];
    }

    public class ItemPedidoInput
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}