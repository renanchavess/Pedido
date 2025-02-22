using GerenciadorPedidos.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Core.Repositories
{
    public interface IProdutoRepository
    {        
        public Task<Produto> ObterPorIdAsync(int id);
        public Task<Produto> CriarAsync(Produto Produto);
        public Task<List<Produto>> ListarTodosAsync();
    }
}
