using GerenciadorPedidos.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciadorPedidos.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            return services;
        }
    }
}
