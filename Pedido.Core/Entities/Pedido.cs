using GerenciadorPedidos.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Core.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public List<Produto>? Produtos { get; set; }
        public PedidoStatus Status { get; set; }

        public Pedido()
        {
            Data = DateTime.Now;
            Status = PedidoStatus.Aberto;
        }
    }
}
