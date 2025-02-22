using GerenciadorPedidos.Core.Repositories;
using GerenciadorPedidos.Infrastructure.Context;
using GerenciadorPedidos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorPedidos.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInsfrasctructure(this IServiceCollection services)
        {
            return services
                .AddDbContext()
                .AddRepositories();
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            // string sqlServerConnection = Environment.GetEnvironmentVariable("SqlServerConnection");
            services.AddDbContext<PedidosDbContext>(options =>
            {
                options.UseInMemoryDatabase("PedidosDb");
            });
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
