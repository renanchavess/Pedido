using GerenciadorPedidos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorProdutos.Application.DTOs.Input
{
    public class ProdutoInputModel
    {
        public required string Nome { get; set; }
        public required decimal Preco { get; set; }

        public static Produto ToProduto(ProdutoInputModel ProdutoInputModel)
        {
            return new Produto
            {
                Nome = ProdutoInputModel.Nome,
                Preco = ProdutoInputModel.Preco
            };
        }
    }
}
