using GerenciadorPedidos.Core.Entities;
using GerenciadorPedidos.Core.Enums;
using GerenciadorPedidos.Core.Repositories;
using GerenciadorPedidos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly PedidosDbContext _context;

        public PedidoRepository(PedidosDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> CriarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> ObterPorIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Produtos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> AtualizarAsync(Pedido pedido)
        {
            _context.Entry(pedido).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Pedido>> ListarTodos(int pagina, int quantidadePorPagina, PedidoStatus? status)
        {
            var query =  _context.Pedidos
                .Include(p => p.Produtos)                
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(p => p.Status == status);
            }

            return await query
                .Skip((pagina - 1) * quantidadePorPagina)
                .Take(quantidadePorPagina)
                .ToListAsync();
        }
    }
}
