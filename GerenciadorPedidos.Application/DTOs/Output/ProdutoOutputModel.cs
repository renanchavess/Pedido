using GerenciadorPedidos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Application.DTOs.Output
{
    public class ProdutoOutputModel
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required decimal Preco { get; set; }

        public static ProdutoOutputModel FromProduto(Produto produto)
        {
            return new ProdutoOutputModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }
    }
}
