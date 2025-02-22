using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GerenciadorPedidos.Application.Services;
using GerenciadorPedidos.Core.Entities;
using GerenciadorPedidos.Core.Enums;

namespace GerenciadorPedidos.Controllers
{
    [Route("api/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido()
        {
            var pedidoCriado = await _pedidoService.CriarPedidoAsync();
            return CreatedAtAction(nameof(ObterPedidoPorId), new { id = pedidoCriado.Id }, pedidoCriado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPedidoPorId(int id)
        {
            var pedido = await _pedidoService.ObterPedidoPorIdAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpPost("{pedidoId}/adicionar-produto")]
        public async Task<IActionResult> AdicionarProduto(int pedidoId, int produtoId)
        {
            bool produtoAdicionado = await _pedidoService.AdicionarProdutoAsync(pedidoId, produtoId);
            if (!produtoAdicionado)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("{pedidoId}/remover-produto")]
        public async Task<IActionResult> RemoverProduto(int pedidoId, int produtoId)
        {
            bool produtoRemovido = await _pedidoService.RemoverProdutoAsync(pedidoId, produtoId);
            if (!produtoRemovido)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("{id}/finalizar")]
        public async Task<IActionResult> FinalizarPedido(int id)
        {
            var pedidoFinalizado = await _pedidoService.FinalizarPedidoAsync(id);
            if (!pedidoFinalizado)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ListarPedidos([FromQuery] int pagina = 1, [FromQuery] PedidoStatus? status = null)
        {
            int quantidadePorPagina = 3;
            var pedidos = await _pedidoService.ListarPedidosAsync(pagina, quantidadePorPagina, status);
            return Ok(pedidos);
        }
    }
}
