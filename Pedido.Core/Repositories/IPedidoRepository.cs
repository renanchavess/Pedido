using GerenciadorPedidos.Core.Entities;
using GerenciadorPedidos.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Core.Repositories
{
    public interface IPedidoRepository
    {
        public Task<Pedido> ObterPorIdAsync(int id);
        public Task<List<Pedido>> ListarTodos(int pagina, int quantidadePorPagina, PedidoStatus? status);
        public Task<Pedido> CriarAsync(Pedido pedido);
        public Task<bool> AtualizarAsync(Pedido pedido);
    }
}
