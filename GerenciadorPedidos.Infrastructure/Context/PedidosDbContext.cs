
using GerenciadorPedidos.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorPedidos.Infrastructure.Context
{
    public class PedidosDbContext: DbContext
    {
        public PedidosDbContext(DbContextOptions<PedidosDbContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pedido>()
            .HasMany(p => p.Produtos)
            .WithMany(p => p.Pedidos)
            .UsingEntity<Dictionary<string, object>>(
                "PedidoProduto",
                j => j.HasOne<Produto>().WithMany().HasForeignKey("ProdutoId"),
                j => j.HasOne<Pedido>().WithMany().HasForeignKey("PedidoId")
            );
        }
    }
}
