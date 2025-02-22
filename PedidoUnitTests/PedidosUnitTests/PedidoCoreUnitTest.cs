using GerenciadorPedidos.Application.Services;
using GerenciadorProdutos.Application.DTOs.Input;
using GerenciadorPedidos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;
using GerenciadorPedidos.Infrastructure.Repositories;
using GerenciadorPedidos.Application.Exceptions;

namespace PedidoUnitTests.PedidosUnitTests
{
    public class PedidoCoreUnitTest
    {
        private readonly IPedidoService _pedidoService;
        private readonly IProdutoService _produtoService;
        private readonly PedidosDbContext _context;

        public PedidoCoreUnitTest()
        {
            var options = new DbContextOptionsBuilder<PedidosDbContext>()
                .UseInMemoryDatabase(databaseName: "PedidosDb")
                .Options;

            _context = new PedidosDbContext(options);

            var pedidoRepository = new PedidoRepository(_context);
            var produtoRepository = new ProdutoRepository(_context);

            _pedidoService = new PedidoService(pedidoRepository, produtoRepository);
            _produtoService = new ProdutoService(produtoRepository);
        }

        [Fact]
        public async Task CriarPedidoAsync()
        {
            var pedido = await _pedidoService.CriarPedidoAsync();
            Assert.NotNull(pedido);
        }

        [Fact]
        public async Task AdicionarProdutoAsync()
        {
            var pedido = await _pedidoService.CriarPedidoAsync();
            var produto = await _produtoService.CriarProdutoAsync(new ProdutoInputModel { Nome = "Produto Teste", Preco = 10 });
            var produtoAdicionado = await _pedidoService.AdicionarProdutoAsync(pedido.Id, produto.Id);
            Assert.True(produtoAdicionado);
        }

        [Fact]
        public async Task RemoverProdutoAsync()
        {
            var pedido = await _pedidoService.CriarPedidoAsync();
            var produto = await _produtoService.CriarProdutoAsync(new ProdutoInputModel { Nome = "Produto Teste", Preco = 10 });
            await _pedidoService.AdicionarProdutoAsync(pedido.Id, produto.Id);
            var produtoRemovido = await _pedidoService.RemoverProdutoAsync(pedido.Id, produto.Id);
            Assert.True(produtoRemovido);
        }

        [Fact]
        public async Task FinalizarPedidoAsync()
        {
            var pedido = await _pedidoService.CriarPedidoAsync();
            var produto = await _produtoService.CriarProdutoAsync(new ProdutoInputModel { Nome = "Produto Teste", Preco = 10 });
            await _pedidoService.AdicionarProdutoAsync(pedido.Id, produto.Id);
            var pedidoFinalizado = await _pedidoService.FinalizarPedidoAsync(pedido.Id);
            Assert.True(pedidoFinalizado);
        }

        [Fact]
        public async Task FinalizarPedidoSemProdutoAsync()
        {
            var pedido = await _pedidoService.CriarPedidoAsync();
            var exception = await Assert.ThrowsAsync<PedidoValidationException>(async () =>
            {
                await _pedidoService.FinalizarPedidoAsync(pedido.Id);
            });

            Assert.Equal("Pedido sem Pedidos não pode ser finalizado", exception.Message);
        }

        [Fact]
        public async Task AdicionarProdutoInexistenteAsync()
        {
            var pedido = await _pedidoService.CriarPedidoAsync();
            var exception = await Assert.ThrowsAsync<PedidoValidationException>(async () =>
            {
                await _pedidoService.AdicionarProdutoAsync(pedido.Id, 1);
            });
            Assert.Equal("Produto não encontrado", exception.Message);
        }

        [Fact]
        public async Task RemoverProdutoInexistenteAsync()
        {
            var pedido = await _pedidoService.CriarPedidoAsync();
            var exception = await Assert.ThrowsAsync<PedidoValidationException>(async () =>
            {
                await _pedidoService.RemoverProdutoAsync(pedido.Id, 1);
            });
            Assert.Equal("Produto não encontrado no pedido", exception.Message);
        }

        [Fact] public async Task RemoverProdutoPedidoFinalizado()
        {
            var pedido = await _pedidoService.CriarPedidoAsync();
            var produto = await _produtoService.CriarProdutoAsync(new ProdutoInputModel { Nome = "Produto Teste", Preco = 10 });
            await _pedidoService.AdicionarProdutoAsync(pedido.Id, produto.Id);
            await _pedidoService.FinalizarPedidoAsync(pedido.Id);
            var exception = await Assert.ThrowsAsync<PedidoValidationException>(async () =>
            {
                await _pedidoService.RemoverProdutoAsync(pedido.Id, produto.Id);
            });
            Assert.Equal("Pedido finalizado não pode ser alterado", exception.Message);
        }
    }
}
