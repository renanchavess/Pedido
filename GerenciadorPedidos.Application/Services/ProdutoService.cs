using GerenciadorPedidos.Application.DTOs.Output;
using GerenciadorPedidos.Core.Entities;
using GerenciadorPedidos.Core.Repositories;
using GerenciadorProdutos.Application.DTOs.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<ProdutoOutputModel> CriarProdutoAsync(ProdutoInputModel ProdutoInput)
        {
            Produto Produto = ProdutoInputModel.ToProduto(ProdutoInput);
            ProdutoOutputModel ProdutoOutput = ProdutoOutputModel.FromProduto(await _produtoRepository.CriarAsync(Produto));
            return ProdutoOutput;
        }

        public async Task<List<ProdutoOutputModel>> ListarProdutosAsync()
        {
            var Produtos =  await _produtoRepository.ListarTodosAsync();
            return Produtos.Select(p => ProdutoOutputModel.FromProduto(p)).ToList();
        }
    }
}
