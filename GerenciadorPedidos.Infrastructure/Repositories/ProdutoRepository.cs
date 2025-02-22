using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorPedidos.Core.Entities;
using GerenciadorPedidos.Core.Repositories;
using GerenciadorPedidos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidos.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly PedidosDbContext _context;

        public ProdutoRepository(PedidosDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> CriarAsync(Produto Produto)
        {
            _context.Produtos.Add(Produto);
            await _context.SaveChangesAsync();
            return Produto;
        }

        public async Task<List<Produto>> ListarTodosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> ObterPorIdAsync(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
