using GerenciadorPedidos.Core.Entities;
using GerenciadorPedidos.Core.Repositories;
using GerenciadorPedidos.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Application.DTOs.Output;
using GerenciadorPedidos.Application.Exceptions;

namespace GerenciadorPedidos.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> AdicionarProdutoAsync(int pedidoId, int produtoId)
        {
            Produto Produto = await _produtoRepository.ObterPorIdAsync(produtoId);
            if (Produto == null)
            {
                throw new PedidoValidationException("Produto não encontrado");
            }

            Pedido pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);
            if (pedido == null)
            {
                throw new PedidoValidationException("Pedido não encontrado");
            }

            if (pedido.Status == PedidoStatus.Finalizado)
            {
                throw new PedidoValidationException("Pedido finalizado não pode ser alterado");
            }

            if (pedido.Produtos == null)
            {
                pedido.Produtos = new List<Produto>();
            }

            pedido.Produtos.Add(Produto);

            return await _pedidoRepository.AtualizarAsync(pedido);
        }

        public async Task<PedidoOutputModel> CriarPedidoAsync()
        {
            Pedido pedido = new Pedido();
            await _pedidoRepository.CriarAsync(pedido);
            return PedidoOutputModel.FromPedido(pedido);
        }

        public async Task<bool> FinalizarPedidoAsync(int id)
        {
            Pedido pedido = await _pedidoRepository.ObterPorIdAsync(id);

            if (pedido == null)
            {
                throw new PedidoValidationException("Pedido não encontrado");
            }

            if (pedido.Produtos.Count == 0)
            {
                throw new PedidoValidationException("Pedido sem Pedidos não pode ser finalizado");
            }

            pedido.Status = PedidoStatus.Finalizado;
            return await _pedidoRepository.AtualizarAsync(pedido);
        }

        public async Task<List<PedidoOutputModel>> ListarPedidosAsync(int pagina, int quantidadePorPagina, PedidoStatus? status)
        {
            var pedidos = await _pedidoRepository.ListarTodos(pagina, quantidadePorPagina, status);
            return pedidos.Select(p => PedidoOutputModel.FromPedido(p)).ToList();
        }

        public async Task<PedidoOutputModel> ObterPedidoPorIdAsync(int id)
        {
            Pedido pedido = await _pedidoRepository.ObterPorIdAsync(id);
            if (pedido == null)
            {
                return null;
            }

            return PedidoOutputModel.FromPedido(pedido);
        }

        public async Task<bool> RemoverProdutoAsync(int pedidoId, int produtoId)
        {
            Pedido  pedido = await _pedidoRepository.ObterPorIdAsync(pedidoId);

            if (pedido == null)
            {
                throw new Exception("Pedido não encontrado");
            }

            Produto produto = pedido.Produtos.FirstOrDefault(p => p.Id == produtoId);

            if (produto == null)
            {
                throw new PedidoValidationException("Produto não encontrado no pedido");
            }

            if (pedido.Status == PedidoStatus.Finalizado)
            {
                throw new PedidoValidationException("Pedido finalizado não pode ser alterado");
            }

            pedido.Produtos.Remove(produto);
            return await _pedidoRepository.AtualizarAsync(pedido);
        }
    }
}
