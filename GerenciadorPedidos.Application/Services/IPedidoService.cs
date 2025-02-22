using GerenciadorPedidos.Application.DTOs.Output;
using GerenciadorPedidos.Core.Entities;
using GerenciadorPedidos.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Application.Services
{
    public interface IPedidoService
    {
        Task<PedidoOutputModel> CriarPedidoAsync();
        Task<bool> AdicionarProdutoAsync(int pedidoId, int produtoId);
        Task<bool> RemoverProdutoAsync(int pedidoId, int produtoId);
        Task<bool> FinalizarPedidoAsync(int id);
        Task<PedidoOutputModel> ObterPedidoPorIdAsync(int id);
        Task<List<PedidoOutputModel>> ListarPedidosAsync(int pagina, int quantidadePorPagina, PedidoStatus? status);
    }
}
