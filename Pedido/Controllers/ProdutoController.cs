using GerenciadorPedidos.Application.DTOs.Output;
using GerenciadorPedidos.Application.Services;
using GerenciadorProdutos.Application.DTOs.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace GerenciadorProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _ProdutoService;

        public ProdutoController(IProdutoService ProdutoService)
        {
            _ProdutoService = ProdutoService;
        }

        [HttpPost]
        public async Task<ProdutoOutputModel> CriarProduto(ProdutoInputModel Produto)
        {
            var ProdutoCriado = await _ProdutoService.CriarProdutoAsync(Produto);
            return ProdutoCriado;
        }

        [HttpGet]
        public async Task<List<ProdutoOutputModel>> ListarProdutos()
        {
            return await _ProdutoService.ListarProdutosAsync();
        }
    }
}
