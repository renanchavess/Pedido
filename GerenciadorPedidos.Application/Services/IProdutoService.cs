using GerenciadorPedidos.Application.DTOs.Output;
using GerenciadorPedidos.Core.Entities;
using GerenciadorProdutos.Application.DTOs.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Application.Services
{
    public interface IProdutoService
    {
        public Task<ProdutoOutputModel> CriarProdutoAsync(ProdutoInputModel Produto);
        public Task<List<ProdutoOutputModel>> ListarProdutosAsync();
    }
}
