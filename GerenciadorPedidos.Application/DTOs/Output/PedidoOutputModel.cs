using GerenciadorPedidos.Core.Entities;
using GerenciadorPedidos.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Application.DTOs.Output
{
    public class PedidoOutputModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public List<ProdutoOutputModel>? Produtos { get; set; }
        public PedidoStatus Status { get; set; }

        public static PedidoOutputModel FromPedido(Pedido pedido)
        {
            return new PedidoOutputModel
            {
                Id = pedido.Id,
                Data = pedido.Data,
                Produtos = pedido.Produtos?.Select(ProdutoOutputModel.FromProduto).ToList(),
                Status = pedido.Status
            };
        }
    }
}
