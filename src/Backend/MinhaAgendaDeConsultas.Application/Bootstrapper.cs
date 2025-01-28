using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaAgendaDeConsultas.Application
{
    public static class Bootstrapper
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AdicionarUseCases(services);

        }

        /*Registrar nas configurações de dependência.*/
        private static void AdicionarUseCases(IServiceCollection services)
        {
            //services.AddScoped<IRegistrarContatoUseCase, RegistrarContatoUseCase>()
            //    .AddScoped<IRecuperarPorPrefixoUseCase, RecuperarPorPrefixoUseCase>()
            //    .AddScoped<IRecuperarDDDRegiaoPorPrefixoUseCase, RecuperarDDDRegiaoPorPrefixoUseCase>()
            //    .AddScoped<IRecuperarPorIdUseCase, RecuperarPorIdUseCase>()
            //    .AddScoped<IRecuperarTodosContatosUseCase, RecuperarTodosContatosUseCase>()
            //    .AddScoped<IUpdateContatoUseCase, UpdateContatoUseCase>()
            //    .AddScoped<IDeletarContatoUseCase, DeletarContatoUseCase>();

        }
    }
}
